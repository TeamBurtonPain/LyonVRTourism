using System;
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
    public static string ToJsonString(Quest q)
    {
        JObject jsonQuest = new JObject(
            new JProperty("_idCreator", q.IdCreator),
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

    public static Quest ToQuest(string questJson)
    {
        JObject parse = JObject.Parse(questJson);
        string idCreator = (string)parse["_idCreator"];
        Coordinates geolocalisation = new Coordinates((float)parse["geolocalisation"]["x"], (float)parse["geolocalisation"]["y"]);
        string title = (string)parse["title"];
        string description = (string)parse["description"];
        List<CheckPoint> checkpoints = ToListCheckpoint((JArray)parse["checkpoints"]);
        long value = (long)parse["value"];
        Quest quest = new Quest(geolocalisation, title, description, value, idCreator, checkpoints);
        return quest;
    }


    public static List<CheckPoint> ToListCheckpoint(JArray checkpointsArray)
    {
        List<CheckPoint> checkpoints = new List<CheckPoint>();
        foreach (var item in checkpointsArray)
        {
            JObject parse = JObject.Parse(item.ToString());
            string text = (string)parse["text"];
            JArray choicesArray = (JArray)parse["choices"];
            List<string> choices = new List<string>();
            foreach (var choice in choicesArray)
            {
                choices.Add(choice.ToString());
            }
            string answer = (string)parse["answer"];
            int difficulty = (int)parse["difficulty"];
            CheckPoint checkPoint = new CheckPoint(null, text, choices, answer, difficulty);
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

    public static Account ToAccount(string accountJson)
    {
        JObject parse = JObject.Parse(accountJson);
        string mail = (string)parse["connection"]["email"];
        string password = (string)parse["connection"]["password"];
        string firstName = (string)parse["userInformation"]["firstname"];
        string lastName = (string)parse["userInformation"]["lastname"];
        RoleAccount roleAccount = (RoleAccount)Enum.Parse(typeof(RoleAccount), (string)parse["userInformation"]["accountType"]);
        DateTime creationDate = (DateTime)parse["createdAt"];
        DateTime updateDate = (DateTime)parse["updatedAt"];
        long elapsedTime = (long)parse["game"]["elapsedTime"];
        User user = ToUser(accountJson);
        Account account = new Account(user, mail, password, firstName, lastName, roleAccount, creationDate, updateDate, elapsedTime);
        return account;
    }

    public static User ToUser(string userJson)
    {
        JObject parse = JObject.Parse(userJson);
        string username = (string)parse["userInformation"]["username"];
        string id = (string)parse["_id"];
        long xp = (long)parse["game"]["xp"];
        //TODO Remplir ces champs !
        List<Badge> badges = new List<Badge>();
        Dictionary<long, StateQuest> quests = new Dictionary<long, StateQuest>();
        User user = new User(username, id, xp, badges, quests);
        return user;
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

