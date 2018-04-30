using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;


public static class JSONHelper
{
    //****************************** QUEST ******************************//
    public static string ToJsonString(Quest q)
    {
        JObject jsonQuest = new JObject(
            //new JProperty("_id", q.Id),
            new JProperty("_idCreator", q.Creator.Id),
            new JProperty("geolocalisation", JSONHelper.ToJson(q.Geolocalisation)),
            new JProperty("title", q.Title),
            new JProperty("description", q.Description),
            //new JProperty("picture", q.picture64),
            new JProperty("checkpoints", JSONHelper.ToJson(q.Checkpoints)),
            new JProperty("createdAt", q.CreationDate),
            new JProperty("updatedAt", q.UpdateDate),
            new JProperty("statistics", JSONHelper.ToJson(q.Statistics)),
            new JProperty("open", q.Open)
        );
        return jsonQuest.ToString();
    }



    public static JObject ToJson(Coordinates c)
    {
        JObject jsonCoord = new JObject(
            new JProperty("x", c.x),
            new JProperty("y", c.y)
        );
        return jsonCoord;
    }
    public static JArray ToJson(List<CheckPoint> cps)
    {
        JArray jsonCheckpoints = new JArray();
        foreach (var checkPoint in cps)
        {
            jsonCheckpoints.Add(
                JSONHelper.ToJson(checkPoint)
            );
        }

        return jsonCheckpoints;

    }

    public static JObject ToJson(CheckPoint c)
    {
        JObject jsonCheckpoint = new JObject(
            //new JProperty("photo", photo64),
            new JProperty("text", c.Text),
            new JProperty("choices", new JArray(c.Choices)),
            new JProperty("answer", c.Answer),
            new JProperty("difficulty", c.Difficulty)

        );
        return jsonCheckpoint;
    }

    public static JArray ToJson(QuestStatistics qs)
    {
        JArray jsonStatistics = new JArray();
        foreach (var qsu in qs.Marks)
        {
            jsonStatistics.Add(JSONHelper.ToJson(qsu));
        }
        return jsonStatistics;
    }

    public static JObject ToJson(QuestStatisticsUnit qsu)
    {
        JObject jsonQsu = new JObject(
            new JProperty("_idUser", qsu.AssociatedUser.Id),
            new JProperty("comment", qsu.Comment),
            new JProperty("mark", qsu.Mark)
        );
        return jsonQsu;
    }


    //****************************** USER ******************************//

    public static string GetIDFromAuth(string auth)
    {
        JObject jsonAuth = JObject.Parse(auth);
        return (string) jsonAuth["_id"];
    }

    public static string ToJsonString(string mail, string pwd)
    {
        JObject jsonConnect = new JObject(
            new JProperty("email", mail),
            new JProperty("password", pwd)
        );
        return jsonConnect.ToString();
    }

    public static string ToJsonString(Account a)
    {
        JObject jsonAccount = new JObject(
            //new JProperty("_id", a.Id),
            new JProperty("connection", new JObject(
                new JProperty("email", a.Mail),
                new JProperty("password", a.Password)
            )),
            new JProperty("userInformation", new JObject(
                new JProperty("lastname", a.LastName),
                new JProperty("firstname", a.FirstName),
                new JProperty("dateOfBirth", a.DateBirth.ToString()),
                new JProperty("username", a.Username),
                new JProperty("accountType", a.Role.ToString())
            )),
            new JProperty("createdAt", a.CreationDate),
            new JProperty("updatedAt", a.UpdateDate),
            new JProperty("game", new JObject(
                new JProperty("badges", JSONHelper.ToJson(a.Badges)),
                new JProperty("quests", JSONHelper.ToJson(a.Quests)),
                new JProperty("xp", a.Xp),
                new JProperty("elapsedTime", a.ElapsedTime)
            ))
        );
        return jsonAccount.ToString();
    }

    public static JArray ToJson(List<Badge> badges)
    {
        JArray jsonBadges = new JArray();
        foreach (var badge in badges)
        {
            jsonBadges.Add(badge.Id);
        }

        return jsonBadges;
    }

    public static JArray ToJson(Dictionary<long, StateQuest> quests)
    {
        JArray jsonQuest = new JArray();
        foreach (var quest in quests)
        {
            jsonQuest.Add(new JObject(
                new JProperty("_idQuest", quest.Key),
                new JProperty("state", quest.Value.Done?"DONE":"IN_PROGRESS"),
                new JProperty("stats", new JObject(
                    new JProperty("earnedXP", quest.Value.Score)
                ))
            ));
        }

        return jsonQuest;
    }

    public static Account GetAccount(string json)
    {
        return new Account();
    }

}

