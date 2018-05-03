using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class JSONHelper : MonoBehaviour
{

    protected static JSONHelper instance;
    public static JSONHelper Instance
    {
        get { return instance; }
    }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

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
            new JProperty("difficulty", c.Difficulty),
            new JProperty("_idBadge", c.Badge)
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

    public IEnumerator ToQuest(string questJson, System.Action<Quest> b)
    {
        JObject parse = JObject.Parse(questJson);
        string idCreator = (string) parse["_idCreator"];

        string id = (string) parse["_id"];
        Coordinates geolocalisation =
            new Coordinates((float) parse["geolocalisation"]["x"], (float) parse["geolocalisation"]["y"]);
        string title = (string) parse["title"];
        string description = (string) parse["description"];
        List<CheckPoint> checkpoints = null;
        yield return JSONHelper.instance.ToListCheckpoint((JArray) parse["checkpoints"], value => checkpoints = value);
        //int experienceEarned = (int)parse["value"];
        int experienceEarned = 0;
        Quest quest = new Quest(id, geolocalisation, title, description, experienceEarned, idCreator, checkpoints);
        b(quest);
    }

    public IEnumerator ToQuests(string questJson, System.Action<List<Quest>> b)
    {
        JArray parse = JArray.Parse(questJson);
        List<Quest> list = new List<Quest>();
        foreach (JToken json in parse)
        {
            Quest q = null;
            yield return ToQuest(JObject.Parse(json.ToString()).ToString(), value => q = value);
            list.Add(q);
        }

        b(list);
    }


    public IEnumerator ToListCheckpoint(JArray checkpointsArray, System.Action<List<CheckPoint>> b)
    {
        List<CheckPoint> checkpoints = new List<CheckPoint>();
        foreach (var item in checkpointsArray)
        {
            JObject parse = JObject.Parse(item.ToString());
            string text = (string) parse["text"];
            string question = (string) parse["question"];
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
            Badge badge = null;
            if ((string)parse["_idBadge"] != "" && (string)parse["_idBadge"] != null)
            {
                yield return HTTPHelper.Instance.GetBadge((string)parse["_idBadge"], value => badge = value);
            }
            CheckPoint checkPoint = new CheckPoint(picture, pictureName, text, question, choices, answer, difficulty, badge);
            checkpoints.Add(checkPoint);
        }

        b(checkpoints);
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

    public static string ToJsonString(Account a, bool creation)
    {
        JObject connection = new JObject(
            new JProperty("email", a.Mail)
        );
        if (creation)
        {
            connection.Add("password", a.Password);
        }
        JObject jsonAccount = new JObject(
            //new JProperty("_id", a.Id),
            new JProperty("connection", connection),
            new JProperty("userInformation", new JObject(
                new JProperty("lastName", a.LastName),
                new JProperty("firstName", a.FirstName),
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
            jsonQuest.Add(JSONHelper.ToJson(quest.Value));
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
            new JProperty("_idQuest", quest.Quest.Id),
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

    public IEnumerator ToAccount(string accountJson, Action<Account> a)
    {
        JObject parse = JObject.Parse(accountJson);
        string mail = (string) parse["connection"]["email"];
        string password = (string) parse["connection"]["password"];
        string firstName = (string) parse["userInformation"]["firstName"];
        string lastName = (string) parse["userInformation"]["lastName"];
        RoleAccount roleAccount =
            (RoleAccount) Enum.Parse(typeof(RoleAccount), (string) parse["userInformation"]["accountType"]);
        DateTime creationDate = (DateTime) parse["createdAt"];
        DateTime updateDate = (DateTime) parse["updatedAt"];
        long elapsedTime = (long) parse["game"]["elapsedTime"];
        User user = new User();
        yield return JSONHelper.Instance.ToUser(accountJson, value => user = value);
        Account account = new Account(user, mail, password, firstName, lastName, roleAccount, creationDate, updateDate,
            elapsedTime);
        a(account);
    }

    public IEnumerator ToUser(string userJson, Action<User> u)
    {
        JObject parse = JObject.Parse(userJson);
        string username = (string) parse["userInformation"]["username"];
        string id = (string) parse["_id"];
        long xp = (long) parse["game"]["xp"];
        //TODO Remplir ces champs !
        List<Badge> badges = new List<Badge>();
        yield return JSONHelper.Instance.ToListBadge(JObject.Parse(parse.GetValue("game").ToString()).GetValue("badges").ToString(), value => badges = value);

        Dictionary<string, StateQuest> quests = new Dictionary<string, StateQuest>();
        yield return JSONHelper.Instance.ToDictState(JObject.Parse(parse.GetValue("game").ToString()).GetValue("quests").ToString(), value => quests = value);


        User user = new User(username, id, xp, badges, quests);
        u(user);
    }

    public IEnumerator ToDictState(string json, Action<Dictionary<string, StateQuest>> d)
    {
        Dictionary<string, StateQuest> dic = new Dictionary<string, StateQuest>();

        JArray jsonQuest = JArray.Parse(json);
        for (int i=0; i < jsonQuest.Count; i++)
        {
            JObject tokenQuest = JObject.Parse(jsonQuest[0].ToString());
            StateQuest sq = null;
            yield return JSONHelper.Instance.ToStateQuest(tokenQuest.ToString(), value => sq = value);
            dic.Add(sq.Quest.Id, sq);
        }

        d(dic);
    }

    public IEnumerator ToStateQuest(string json, System.Action<StateQuest> sq)
    {
        JObject quest = JObject.Parse(json);
        string key = (string)quest["_idQuest"];

        //TODO: Est-ce nécessaire ?


        Quest q = null;
        yield return HTTPHelper.Instance.GetQuest(key, value => q = value);

        bool done = ((string)quest["state"]) == "DONE";
        double score = (double)quest["stats"]["earnedXp"];
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

        sq(new StateQuest(q, done, score, timeElapsed, listCheckPoints));
    }

    //------------------------------------------------------------------------------------------


    
    public  IEnumerator ToListBadge(string badgeArrayJson, System.Action< List<Badge> > b)
    {
        JArray jArray = JArray.Parse(badgeArrayJson);
        List<Badge> badges = new List<Badge>();
        foreach (var item in jArray)
        {
            string id = item.ToString();
            Badge curr = null;
            yield return HTTPHelper.Instance.GetBadge(id, value => curr = value);
            badges.Add(curr);
        }

        b(badges);
    }

    

    public static Badge ToBadge(string json)
    {
        JObject parse = JObject.Parse(json);
        string name = (string) parse["name"];
        string description = (string) parse["description"];
        string icon = (string) parse["picture"];
        string id = (string) parse["_id"];
        long xp = (long) parse["earn"];


        Badge badge = new Badge(id, name, description, xp, icon);
        return badge;
    }


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
