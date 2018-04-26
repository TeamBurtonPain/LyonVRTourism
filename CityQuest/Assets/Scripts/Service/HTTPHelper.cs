using System.Collections;
using System.Collections.Generic;using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class HTTPHelper
{
    public const string SERVER = "http://192.168.43.228:3000/api/";
    public static bool Send(Account a)
    {
        Debug.Log(JSONHelper.ToJsonString(a));
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "accounts", Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(a)));
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        Debug.Log(uwr.ToString());
        uwr.SendWebRequest();
        return true;


        /*
        UnityWebRequest webRequest = UnityWebRequest.Put(destination, formData);
        UploadHandler customUploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(formData));
        customUploadHandler.contentType = "application/json";
        webRequest.uploadHandler = customUploadHandler;
        SendRequest(webRequest);
        */
    }
}