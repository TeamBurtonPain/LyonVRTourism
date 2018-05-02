using System.Text;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

public static class HTTPHelper
{
    //public const string SERVER = "http://192.168.43.228:3000/api/";
    public const string SERVER = "http://192.168.0.11:3000/api/";

    /******************** AUTHENTIFICATION ********************/

    public static Cookie AuthLogin(string mail, string pwd)
    {
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "auth/login", Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(mail, pwd)));
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        uwr.SendWebRequest();
        while (uwr.downloadProgress < 0.95f)
        {
            WaitForSecondsRealtime w = new WaitForSecondsRealtime(0.5f);
        }
        Debug.Log(uwr.ToString());
        byte[] results = uwr.downloadHandler.data;
        string text = uwr.downloadHandler.text;

        Cookie cookie = new Cookie("auth", "");

        if (uwr.responseCode == 200)
        {
            var json = JObject.Parse(text);
            cookie = new Cookie("auth", (string)json["jwt"]);//TODO à tester si erreur ???? json {error : truc, message : truc2}
            Debug.Log(text);
        }
        else// if (uwr.responseCode == 0)
        {
            // ?
        }

        Debug.Log(cookie);
        return cookie;
    }

    public static bool AuthLogout(Cookie cookie)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(SERVER + "auth/logout");
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        uwr.SetRequestHeader("Authorization", "Bearer " + cookie.Value);
        uwr.SendWebRequest();
        while (uwr.downloadProgress < 0.95f)
        {
            WaitForSecondsRealtime w = new WaitForSecondsRealtime(0.5f);
        }
        Debug.Log(uwr.ToString());
        byte[] results = uwr.downloadHandler.data;
        string text = uwr.downloadHandler.text;

        var json = JObject.Parse(text);
        Debug.Log(text);
        if (uwr.responseCode == 200)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    /******************** PERSIST ********************/
    public static bool Persist(Account a)
    {
        Debug.Log(JSONHelper.ToJsonString(a));
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "accounts", Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(a)));
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        uwr.SendWebRequest();
        while (uwr.downloadProgress < 0.95f)
        {
            WaitForSecondsRealtime w = new WaitForSecondsRealtime(0.5f);
        }
        Debug.Log(uwr.ToString());

        if (uwr.responseCode == 200)
        {
            byte[] results = uwr.downloadHandler.data;
            string text = uwr.downloadHandler.text;
            Debug.Log(text);
            return true;
        }
        else
        {
            return false;
        }

    }

    public static bool Persist(Quest q, Cookie cookie)
    {
        Debug.Log(JSONHelper.ToJsonString(q));
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "quests", Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(q)));
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        uwr.SetRequestHeader("Authorization", "Bearer " + cookie.Value);

        Debug.Log(uwr.ToString());
        uwr.SendWebRequest();
        while (uwr.downloadProgress < 0.95f)
        {
            WaitForSecondsRealtime w = new WaitForSecondsRealtime(0.5f);
        }
        Debug.Log(uwr.ToString());
        if (uwr.responseCode == 200)
        {
            byte[] results = uwr.downloadHandler.data;
            string text = uwr.downloadHandler.text;
            Debug.Log(text);
            return true;
        }
        else
        {
            return false;
        }
    }


    /******************** DELETE ********************/

    public static bool Delete(Account a, Cookie cookie)
    {
        UnityWebRequest uwr = UnityWebRequest.Delete(SERVER + "accounts/" + a.Id);
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        Debug.Log(uwr.ToString());
        uwr.SendWebRequest();
        return true;
    }

    public static bool Delete(Quest q, Cookie cookie)
    {
        UnityWebRequest uwr = UnityWebRequest.Delete(SERVER + "quests/" + q.Id);
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        Debug.Log(uwr.ToString());
        uwr.SendWebRequest();
        return true;
    }

    /******************** UPDATE ********************/

    public static bool Update(Account a, Cookie cookie)
    {
        Debug.Log(JSONHelper.ToJsonString(a));
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "accounts/" + a.Id, Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(a)));
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        uwr.SetRequestHeader("Authorization", "Bearer " + cookie.Value);
        uwr.SendWebRequest();
        while (uwr.downloadProgress < 0.95f)
        {
            WaitForSecondsRealtime w = new WaitForSecondsRealtime(0.5f);
        }
        Debug.Log(uwr.ToString());
        string text = uwr.downloadHandler.text;
        Debug.Log(text);
        return true;

    }

    public static bool Update(Quest q, Cookie cookie)
    {
        Debug.Log(JSONHelper.ToJsonString(q));
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "quests/" + q.Id, Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(q)));
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        uwr.SetRequestHeader("Authorization", "Bearer " + cookie.Value);

        Debug.Log(uwr.ToString());
        uwr.SendWebRequest();
        while (uwr.downloadProgress < 0.95f)
        {
            WaitForSecondsRealtime w = new WaitForSecondsRealtime(0.5f);
        }
        Debug.Log(uwr.ToString());
        string text = uwr.downloadHandler.text;
        Debug.Log(text);
        return true;
    }

    /******************** GET ********************/

    public static Account GetAccount(Cookie cookie)
    {
        Debug.Log(cookie.Value);
        string id = JWTHelper.DecodePayload(cookie.Value);
        Debug.Log(id);
        UnityWebRequest uwr = UnityWebRequest.Get(SERVER + "accounts/" + id);
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        uwr.SetRequestHeader("Authorization", "Bearer " + cookie.Value);

        Debug.Log(uwr.ToString());
        uwr.SendWebRequest();
        while (uwr.downloadProgress < 0.95f)
        {
            WaitForSecondsRealtime w = new WaitForSecondsRealtime(0.5f);
        }
        Debug.Log(uwr.ToString());
        string text = uwr.downloadHandler.text;

        Debug.Log(text);
        if (uwr.responseCode == 200)
        {
            return JSONHelper.ToAccount(text);
        }
        else
        {
            JObject json = JObject.Parse(text);
            //Error("Erreur " + uwr.responseCode + ", " + json["message"]);
            return new Account() { LastName = "Erreur " + uwr.responseCode + ", " + json["message"] };
        }
    }

    public static Quest GetQuest(string id, Cookie cookie)
    {
        Debug.Log(cookie.Value);
        Debug.Log(id);
        UnityWebRequest uwr = UnityWebRequest.Get(SERVER + "quests/" + id);
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        uwr.SetRequestHeader("Authorization", "Bearer " + cookie.Value);


        Debug.Log(uwr.ToString());
        uwr.SendWebRequest();

        while (uwr.downloadProgress < 0.95f)
        {
            WaitForSecondsRealtime w = new WaitForSecondsRealtime(0.5f);
        }
        Debug.Log(uwr.ToString());
        string text = uwr.downloadHandler.text;
        Debug.Log(text);

        return JSONHelper.ToQuest(text);
    }

    public static List<Quest> GetAllQuests()
    {
        
        UnityWebRequest uwr = UnityWebRequest.Get(SERVER + "quests/");
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        Debug.Log(uwr.ToString());
        uwr.SendWebRequest();
        Debug.Log("b1. " + System.DateTime.Now);
        while (uwr.downloadProgress < 0.95f)
        {
            WaitForSecondsRealtime w = new WaitForSecondsRealtime(0.5f);
        }
        Debug.Log("b2. " + System.DateTime.Now);
        Debug.Log(uwr.ToString());
        string text = uwr.downloadHandler.text;
        Debug.Log(text);


        if (uwr.responseCode == 200)
        {
            return JSONHelper.ToQuests(text);

        }
        else
        {
            return new List<Quest>();
        }
    }

    // TODO utiliser des ptain de coroutines pour pas avoir un systeme bloquant........
    /*
     
        List<Quest> r;
        StartCoroutine(TestGetAllQuests(value => r = value));
        Debug.Log("end.");
        return null;

         */
         /*
    public IEnumerator TestGetAllQuests(System.Action<List<Quest>> callback)
    {
        Debug.Log("a1. " + System.DateTime.Now);
        UnityWebRequest uwr = UnityWebRequest.Get(SERVER + "quests/");
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
            callback(null);
        }
        else
        {
            Debug.Log("ok");
            string text = uwr.downloadHandler.text;
            callback(JSONHelper.ToQuests(text));
        }
        Debug.Log("a2. " + System.DateTime.Now);
    }
    */
    public static bool Send(Quest q)
    {
        Debug.Log(JSONHelper.ToJsonString(q));
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "quests", Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(q)));
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        Debug.Log(uwr.ToString());
        uwr.SendWebRequest();
        return true;
    }

}
