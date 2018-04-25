using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public static class JSONHelper
{
    public static string ToJson(Quest q)
    {
        List<String> jsonObject = new List<string>();
        jsonObject.Add(ToJson(q.Creator));
        jsonObject.Add(ToJson(q.Geolocalisation));
        jsonObject.Add(JsonUtility.ToJson(q.Title));
        jsonObject.Add(JsonUtility.ToJson(q.Description));
        return "{"+string.Join(",", jsonObject.ToArray())+"}";
    }

    public static string ToJson(Creator c)
    {
        return "\"_idCreator\":\"" + c.Id + "\"";
    }

    public static string ToJson(Coordinates c)
    {
        List<String> jsonObject = new List<string>();
        jsonObject.Add(JsonUtility.ToJson(c.x));
        jsonObject.Add(JsonUtility.ToJson(c.y));
        return "\"geolocalisation\":{"+string.Join(",", jsonObject.ToArray())+"}";
    }
}

