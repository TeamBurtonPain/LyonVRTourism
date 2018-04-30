using System.Collections;
using System.Collections.Generic;using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class HTTPHelper
{
    public const string SERVER = "http://localhost:3000/api/";




    /******************** AUTHENTIFICATION ********************/

    public static Cookie AuthLogin(string mail, string pwd)
    {
        return new Cookie();
    }

    /******************** PERSIST ********************/
    public static bool Persist(Account a)
    {
        Debug.Log(JSONHelper.ToJsonString(a));
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "accounts", Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(a)));
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        Debug.Log(uwr.ToString());
        uwr.SendWebRequest();
        return true;

    }

    public static bool Persist(Quest q)
    {
        Debug.Log(JSONHelper.ToJsonString(q));
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "quests", Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(q)));
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        Debug.Log(uwr.ToString());
        uwr.SendWebRequest();
        return true;
    }


    /******************** DELETE ********************/

    public static bool Delete(Account a)
    {
        UnityWebRequest uwr = UnityWebRequest.Delete(SERVER + "accounts/"+a.Id);
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        Debug.Log(uwr.ToString());
        uwr.SendWebRequest();
        return true;
    }

    public static bool Delete(Quest q)
    {
        UnityWebRequest uwr = UnityWebRequest.Delete(SERVER + "quests/" + q.Id);
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        Debug.Log(uwr.ToString());
        uwr.SendWebRequest();
        return true;
    }
}




