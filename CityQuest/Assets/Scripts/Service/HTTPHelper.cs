using System.Text;
﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class HTTPHelper : MonoBehaviour
{
    protected static HTTPHelper instance;
    public static HTTPHelper Instance
    {
        get { return instance; }
    }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    public const string SERVER = "http://192.168.43.228:3000/api/";
    //public const string SERVER = "http://192.168.0.11:3000/api/";

    /******************** AUTHENTIFICATION ********************/

    public IEnumerator AuthLogin(string mail, string pwd, System.Action<Cookie> callback)
    {
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "auth/login", Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(mail, pwd)));
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        yield return uwr.SendWebRequest();

        Cookie cookie = new Cookie("auth", "");

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Controller.Instance.Error(uwr.error);
            callback(cookie);
        }
        else
        {

            byte[] results = uwr.downloadHandler.data;
            string text = uwr.downloadHandler.text;
            var json = JObject.Parse(text);
            cookie = new Cookie("auth", (string)json["jwt"]);//TODO à tester si erreur ???? json {error : truc, message : truc2}
            Debug.Log(text);

            callback(cookie);

            Debug.Log(cookie);
        }
    }

    public IEnumerator AuthLogout(Cookie cookie)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(SERVER + "auth/logout");
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        uwr.SetRequestHeader("Authorization", "Bearer " + cookie.Value);

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Controller.Instance.Error(uwr.error);
        }
        else
        {
            byte[] results = uwr.downloadHandler.data;
            string text = uwr.downloadHandler.text;
            Debug.Log(text);
        }
    }

    /******************** PERSIST ********************/
    public IEnumerator Persist(Account a, System.Action<bool> callback)
    {
        Debug.Log(JSONHelper.ToJsonString(a));
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "accounts", Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(a)));
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Controller.Instance.Error(uwr.error);
            callback(false);
        }
        else
        {
            byte[] results = uwr.downloadHandler.data;
            string text = uwr.downloadHandler.text;
            Debug.Log(text);
            callback(true);
        }
    }

    public IEnumerator Persist(Quest q, Cookie cookie, System.Action<bool> callback)
    {
        Debug.Log(JSONHelper.ToJsonString(q, true));
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "quests", Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(q, true)));
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        uwr.SetRequestHeader("Authorization", "Bearer " + cookie.Value);

        Debug.Log(uwr.ToString());

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Controller.Instance.Error(uwr.error);
            callback(false);
        }
        else
        {
            byte[] results = uwr.downloadHandler.data;
            string text = uwr.downloadHandler.text;
            Debug.Log(text);
            callback(true);
        }
    }


    /******************** DELETE ********************/

    public IEnumerator Delete(Account a, Cookie cookie)
    {
        UnityWebRequest uwr = UnityWebRequest.Delete(SERVER + "accounts/" + a.Id);
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        Debug.Log(uwr.ToString());
        uwr.SendWebRequest();

        yield return uwr.SendWebRequest();
    }

    public IEnumerator Delete(Quest q, Cookie cookie)
    {
        UnityWebRequest uwr = UnityWebRequest.Delete(SERVER + "quests/" + q.Id);
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        Debug.Log(uwr.ToString());

        yield return uwr.SendWebRequest();
    }

    /******************** UPDATE ********************/

    public IEnumerator UpdateData(Account a, Cookie cookie)
    {
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "accounts/" + a.Id, Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(a)));
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        uwr.SetRequestHeader("Authorization", "Bearer " + cookie.Value);

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Controller.Instance.Error(uwr.error);
        }
        else
        {
            string text = uwr.downloadHandler.text;
            Debug.Log(text);
        }

    }

    public IEnumerator UpdateData(Quest q, Cookie cookie)
    {
        Debug.Log(JSONHelper.ToJsonString(q, true));
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "quests/" + q.Id, Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(q, true)));
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        uwr.SetRequestHeader("Authorization", "Bearer " + cookie.Value);

        Debug.Log(uwr.ToString());
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Controller.Instance.Error(uwr.error);
        }
        else
        {
            string text = uwr.downloadHandler.text;
            Debug.Log(text);
        }
    }

    /******************** GET ********************/

    public IEnumerator GetAccount(Cookie cookie, System.Action<Account> callback)
    {
        Debug.Log(cookie.Value);
        string id = JWTHelper.DecodePayload(cookie.Value);
        UnityWebRequest uwr = UnityWebRequest.Get(SERVER + "accounts/" + id);
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        uwr.SetRequestHeader("Authorization", "Bearer " + cookie.Value);

        Debug.Log(uwr.ToString());

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            //Controller.Instance.Error(uwr.error);
            string text = uwr.downloadHandler.text;
            JObject json = JObject.Parse(text);
            callback(new Account() { LastName = "Erreur " + uwr.responseCode + ", " + json["message"] });
        }
        else
        {
            string text = uwr.downloadHandler.text;
            Debug.Log(uwr.ToString());
            Account account = null;
            yield return JSONHelper.Instance.ToAccount(text, value => account = value);
            callback(account);
        }
        
    }

	
    public IEnumerator GetQuest(string id, System.Action<Quest> callback)
    {

        UnityWebRequest uwr = UnityWebRequest.Get(SERVER + "quests/" + id);
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        yield return uwr.SendWebRequest();
 
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Controller.Instance.Error(uwr.error);
            callback(null);
        }
        else
        {
            string text = uwr.downloadHandler.text;
            Debug.Log(uwr.ToString());
            Quest q = null;
            yield return JSONHelper.Instance.ToQuest(text, value => q = value);
            callback(q);
        }
    }
        
    public IEnumerator GetAllQuests(System.Action<List<Quest>> callback)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(SERVER + "quests/");
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Controller.Instance.Error(uwr.error);
            callback(null);
        }
        else
        {
            string text = uwr.downloadHandler.text;
            List<Quest> q = new List<Quest>();
            yield return JSONHelper.Instance.ToQuests(text, value => q = value);
            callback(q);
        }
    }
    
    public IEnumerator GetBadge(string id, System.Action<Badge> callback)
    {

        UnityWebRequest uwr = UnityWebRequest.Get(SERVER + "badges/" + id);
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        Debug.Log(uwr.ToString());

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Controller.Instance.Error(uwr.error);
            callback(null);
        }
        else
        {
            string text = uwr.downloadHandler.text;
            Debug.Log(uwr.ToString());
            callback(JSONHelper.ToBadge(text));
        }
    }
    

    /******************** SEND ********************/

    public IEnumerator Send(Quest q)
    {
        Debug.Log(JSONHelper.ToJsonString(q, true));
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "quests", Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(q, true)));
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        Debug.Log(uwr.ToString());
        yield return uwr.SendWebRequest();
    }



    
}
