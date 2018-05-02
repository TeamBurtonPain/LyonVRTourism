﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class JSONHelper
{
    //****************************** QUEST ******************************//
    public static string ToJsonString(Quest q, bool withPicture)
    {
        JObject jsonQuest = new JObject(
            new JProperty("_idCreator", q.IdCreator),
            new JProperty("geolocalisation", JSONHelper.ToJson(q.Geolocalisation)),
            new JProperty("title", q.Title),
            new JProperty("description", q.Description),
            new JProperty("checkpoints", JSONHelper.ToJson(q.Checkpoints, withPicture)),
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

    public static JArray ToJson(List<CheckPoint> cps, bool withPicture)
    {
        JArray jsonCheckpoints = new JArray();
        foreach (var checkPoint in cps)
        {
            jsonCheckpoints.Add(
                JSONHelper.ToJson(checkPoint, withPicture)
            );
        }

        return jsonCheckpoints;
    }

    public static JObject ToJson(CheckPoint c, bool withPicture)
    {
        JObject jsonCheckpoint = new JObject(
            new JProperty("text", c.Text),
            new JProperty("question", c.Question),
            new JProperty("choices", new JArray(c.Choices)),
            new JProperty("enigmAnswer", c.Answer),
            new JProperty("difficulty", c.Difficulty)
        );
        if (withPicture)
        {
            jsonCheckpoint.Add(
                new JProperty("picture", c.Picture)    
            );
        }
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

    public static Quest ToQuest(string questJson)
    {
        JObject parse = JObject.Parse(questJson);
        string idCreator = (string) parse["_idCreator"];
        Coordinates geolocalisation =
            new Coordinates((float) parse["geolocalisation"]["x"], (float) parse["geolocalisation"]["y"]);
        string title = (string) parse["title"];
        string description = (string) parse["description"];
        List<CheckPoint> checkpoints = ToListCheckpoint((JArray) parse["checkpoints"]);
        //int experienceEarned = (int)parse["value"];
        int experienceEarned = 0;
        Quest quest = new Quest(geolocalisation, title, description, experienceEarned, idCreator, checkpoints);
        return quest;
    }

    public static List<Quest> ToQuests(string questJson)
    {
        JArray parse = JArray.Parse(questJson);
        List<Quest> list = new List<Quest>();
        foreach (JToken json in parse)
        {
            list.Add(ToQuest(JObject.Parse(json.ToString()).ToString()));
        }

        return list;
    }


    public static List<CheckPoint> ToListCheckpoint(JArray checkpointsArray)
    {
        List<CheckPoint> checkpoints = new List<CheckPoint>();
        foreach (var item in checkpointsArray)
        {
            JObject parse = JObject.Parse(item.ToString());
            string text = (string) parse["text"];
            //TODO: est-ce que ça plante ici quand on ajoute les images (qui ne sont pas envoyées dans le cas de getAllQuests !)
            string picture = (string) parse["picture"];
            string pictureName = (string) parse["pictureName"];
            JArray choicesArray = (JArray) parse["choices"];
            List<string> choices = new List<string>();
            foreach (var choice in choicesArray)
            {
                choices.Add(choice.ToString());
            }

            string answer = (string) parse["enigmAnswer"];
            int difficulty = (int) parse["difficulty"];
            CheckPoint checkPoint = new CheckPoint(picture, pictureName, text, choices, answer, difficulty);
            checkpoints.Add(checkPoint);
        }

        return checkpoints;
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
                //new JProperty("dateOfBirth", a.DateBirth.ToString("s")),
                new JProperty("username", a.Username),
                new JProperty("accountType", a.Role.ToString())
            )),
            new JProperty("game", new JObject(
                new JProperty("badges", JSONHelper.ToJson(a.Badges)),
                new JProperty("quests", JSONHelper.ToJson(a.Quests)),
                new JProperty("xp", a.Xp),
                new JProperty("elapsedTime", a.ElapsedTime)
            ))
        );
        return jsonAccount.ToString();
    }

    public static string ToJsonString(User u)
    {
        JObject jsonAccount = new JObject(
            new JProperty("userInformation", new JObject(
                new JProperty("username", u.Username)
            )),
            new JProperty("game", new JObject(
                new JProperty("badges", JSONHelper.ToJson(u.Badges)),
                new JProperty("quests", JSONHelper.ToJson(u.Quests)),
                new JProperty("xp", u.Xp)
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

    public static JArray ToJson(Dictionary<string, StateQuest> quests)
    {
        JArray jsonQuest = new JArray();
        foreach (var quest in quests)
        {
            jsonQuest.Add(new JObject(
                new JProperty("_idQuest", quest.Key),
                new JProperty("state", quest.Value.Done ? "DONE" : "IN_PROGRESS"),
                new JProperty("stats", new JObject(
                    new JProperty("earnedXP", quest.Value.Score)
                ))
            ));
        }

        return jsonQuest;
    }

    public static JObject ToJson(StateQuest quest)
    {
        JArray jsonCheckpoints = new JArray();
        foreach (var questCheckpoint in quest.Checkpoints)
        {
            jsonCheckpoints.Add(JSONHelper.ToJson(questCheckpoint));
        }

        JObject jsonState = new JObject(
            new JProperty("_idQuest", quest.Quest),
            new JProperty("state", quest.Done ? "DONE" : "IN_PROGRESS"),
            new JProperty("stats", new JObject(
                new JProperty("earnedXP", quest.Score),
                new JProperty("timeElapsed", quest.TimeElapsed)
            )),
            new JProperty("checkpoints", jsonCheckpoints)
        );
        return jsonState;
    }

    public static JObject ToJson(StateCheckPoint checkPoint)
    {
        return new JObject(
            new JProperty("status", checkPoint.Status.ToString()),
            new JProperty("timeElapsed", checkPoint.TimeElapsed)
        );
    }

    public static Account ToAccount(string accountJson)
    {
        JObject parse = JObject.Parse(accountJson);
        string mail = (string) parse["connection"]["email"];
        string password = (string) parse["connection"]["password"];
        string firstName = (string) parse["userInformation"]["firstname"];
        string lastName = (string) parse["userInformation"]["lastname"];
        RoleAccount roleAccount =
            (RoleAccount) Enum.Parse(typeof(RoleAccount), (string) parse["userInformation"]["accountType"]);
        DateTime creationDate = (DateTime) parse["createdAt"];
        DateTime updateDate = (DateTime) parse["updatedAt"];
        long elapsedTime = (long) parse["game"]["elapsedTime"];
        User user = ToUser(accountJson);
        Account account = new Account(user, mail, password, firstName, lastName, roleAccount, creationDate, updateDate,
            elapsedTime);
        return account;
    }

    public static User ToUser(string userJson)
    {
        JObject parse = JObject.Parse(userJson);
        string username = (string) parse["userInformation"]["username"];
        string id = (string) parse["_id"];
        long xp = (long) parse["game"]["xp"];
        //TODO Remplir ces champs !
        List<Badge> badges = new List<Badge>();
        Dictionary<string, StateQuest> quests = JSONHelper.ToDictState(parse.GetValue("quest").ToString());


        User user = new User(username, id, xp, badges, quests);
        return user;
    }

    public static Dictionary<string, StateQuest> ToDictState(string json)
    {
        Dictionary<string, StateQuest> dic = new Dictionary<string, StateQuest>();

        JArray jsonQuest = JArray.Parse(json);
        foreach (var tokenQuest in jsonQuest)
        {
            StateQuest sq = JSONHelper.ToStateQuest(tokenQuest.ToString());

            dic.Add(sq.Quest.Id, sq);
        }

        return dic;
    }

    public static StateQuest ToStateQuest(string json)
    {
        JObject quest = JObject.Parse(json);
        string key = (string)quest["_isQuest"];

        //TODO: Est-ce nécessaire ?
        Quest q = HTTPHelper.GetQuest(key);


        bool done = ((string)quest["state"]) == "DONE";
        double score = (double)quest["stats"]["earnedXP"];
        double timeElapsed = (double)quest["stats"]["timeElapsed"];

        List<StateCheckPoint> listCheckPoints = new List<StateCheckPoint>();

        JArray checkpoints = JArray.Parse(quest.GetValue("checkpoints").ToString());
        int i = 0;
        foreach (var tokenCheckpoint in checkpoints)
        {
            JObject checkpoint = JObject.Parse(tokenCheckpoint.ToString());
            StatusCheckPoint status =
                (StatusCheckPoint)Enum.Parse(typeof(StatusCheckPoint), (string)checkpoint["status"], true);
            double timeCheckpoint = (double)checkpoint["timeElapsed"];

            listCheckPoints.Add(new StateCheckPoint(q.Checkpoints[i], status, timeCheckpoint));

            i++;
        }

        return new StateQuest(q, done, score, timeElapsed, listCheckPoints);
    }

    //------------------------------------------------------------------------------------------


    //public static DateTime ToDateTime(string dateTimeString)
    //{
    //    CultureInfo  provider = new CultureInfo("fr-FR");
    //    string format = "s"; //Correspond au format ISO 8601
    //    //DateTime dateTime = DateTime.Parse(dateTimeString, null, DateTimeStyles.RoundtripKind);
    //    DateTime d;
    //    DateTime.TryParseExact(
    //        dateTimeString,
    //        "s",
    //        CultureInfo.InvariantCulture,
    //        DateTimeStyles.AssumeUniversal, out d);
    //    //Debug.Log(d.ToString());
    //    return d;
    //}


    /*
    public static List<Badge> ToListBadge(string badgeArrayJson)
    {
        JArray jArray = JArray.Parse(badgeArrayJson);
        // La récupération va se faire en deux fois : on a récupéré les ID par le User, ensuite il faut refaire une requête en envoyant les ID pour récupérer l'ensemble des infos
        // il faut rajouter un constructeur pour qu'on ne re créer pas un nouvel Id pour un badge qui existe déjà
        foreach (JObject item in jArray)
        {
            //string name = item.GetValue("name");
            //string url = item.GetValue("url");
            // ...
        }
    }
    */
    /*
    public static Dictionary<long, StateQuest> ToDictionnaryQuest()
    {
        //Il faut déjà pouvoir réucpérer les objets Quest
    }
    */


    //****************************** PICTURE ******************************//

    public static string GetBase64(string img)
    {
        byte[] imageBytes = System.IO.File.ReadAllBytes(img);
        // Convert byte[] to Base64 String
        return Convert.ToBase64String(imageBytes);
    }

    public static byte[] FromBase64(string img64)
    {
        return Convert.FromBase64String(img64);
    }
}
