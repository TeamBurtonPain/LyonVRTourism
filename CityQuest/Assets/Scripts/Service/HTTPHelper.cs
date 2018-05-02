using System.Text;
﻿using System.Collections;
using System.Collections.Generic;using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;

public static class HTTPHelper
{
    public const string SERVER = "http://192.168.43.228:3000/api/";


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

        var json = JObject.Parse(text);

        Cookie cookie;

        if (uwr.responseCode == 200)
        {

            cookie = new Cookie("auth", (string)json["jwt"]);//TODO à tester si erreur ???? json {error : truc, message : truc2}
        }
        else
        {
            cookie = new Cookie("auth", "");
        }
        Debug.Log(text);
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
        byte[] results = uwr.downloadHandler.data;
        string text = uwr.downloadHandler.text;
        Debug.Log(text);
        return true;

    }

    public static bool Persist(Quest q, Cookie cookie)
    {
        Debug.Log(JSONHelper.ToJsonString(q));
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "quests", Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(q)));
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        uwr.SetRequestHeader("Authorization", "Bearer "+cookie.Value);

        Debug.Log(uwr.ToString());
        uwr.SendWebRequest();
        while (uwr.downloadProgress < 0.95f)
        {
            WaitForSecondsRealtime w = new WaitForSecondsRealtime(0.5f);
        }
        Debug.Log(uwr.ToString());
        byte[] results = uwr.downloadHandler.data;
        string text = uwr.downloadHandler.text;
        Debug.Log(text);
        return true;
    }


    /******************** DELETE ********************/

    public static bool Delete(Account a, Cookie cookie)
    {
        UnityWebRequest uwr = UnityWebRequest.Delete(SERVER + "accounts/"+a.Id);
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
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "accounts/"+a.Id, Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(a)));
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
            return new Account(){LastName = "Erreur " + uwr.responseCode + ", " + json["message"] };
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
