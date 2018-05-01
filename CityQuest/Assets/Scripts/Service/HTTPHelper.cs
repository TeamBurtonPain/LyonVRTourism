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
    /*
    public static Quest Get(long id)
    {
        // A implémenter dans le JsonHelper
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "quests", Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(id)));
        uwr.method = "GET";

        uwr.SendWebRequest();
        byte[] results = uwr.downloadHandler.data;
        //A voir comment on récupère exactement le JSON dans le corps de la réponse
        return JSONHelper.ToQuest(results.ToString());
    }

    public static Account Get(string mail, string password)
    {
        // A implémenter dans le JsonHelper
        UnityWebRequest uwr = UnityWebRequest.Put(SERVER + "accounts", Encoding.UTF8.GetBytes(JSONHelper.ToJsonString(mail,password)));
        uwr.method = "GET";

        uwr.SendWebRequest();
        byte[] results = uwr.downloadHandler.data;
        //A voir comment on récupère exactement le JSON dans le corps de la réponse
        return JSONHelper.ToAccount(results.ToString());
    }
    */
}