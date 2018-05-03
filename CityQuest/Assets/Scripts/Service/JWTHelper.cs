    using System;
    using System.Text;
    using Newtonsoft.Json.Linq;

public static class JWTHelper
{
    public static string DecodePayload(string jwt)
    {
        string[] tab = jwt.Split('.');
        string payload = tab[1];
        if (payload.Length % 4 > 0)
            payload = payload.PadRight(payload.Length + 4 - payload.Length % 4, '=');
        byte[] data = Convert.FromBase64String(payload);
        string decodedString = Encoding.UTF8.GetString(data);
        JObject json = JObject.Parse(decodedString);
        return (string)json["accountId"];
    }
}
