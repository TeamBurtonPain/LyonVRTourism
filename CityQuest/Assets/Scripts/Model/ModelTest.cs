using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;

class ModelTest : MonoBehaviour
{
    public Text tquest;
    public Text tuser;

    public IEnumerator Start()
    {
        Creator c = new Creator
        {
            Id = "0@bob",
            Username = "bob",
            Mail = "aemil@cdi.fkr",
            Password = "pass",
            LastName = "fdibs",
            FirstName = "bob",
            Xp = 0L,
            Role = RoleAccount.CREATOR
        };

        CheckPoint cp1 = new CheckPoint
        {
            Picture = "/pic",
            Text = "Text of checkpoint",
            Choices = { "choix 1", "choix2", "choix3" },
            Answer = "choix1"
        };
        CheckPoint cp2 = new CheckPoint
        {
            Picture = "/pic",
            Text = "Text of checkpoint",
            Choices = { "choix 1", "choix2", "choix3" },
            Answer = "choix2"
        };
        CheckPoint cp3 = new CheckPoint
        {
            Picture = "/pic",
            Text = "Text of checkpoint",
            Choices = { "choix 1", "choix2", "choix3" },
            Answer = "choix3"
        };

        Quest q1 = new Quest
        {
            Title = "quete1",
            Description = "Super quete cool",
            Geolocalisation = { x = 10, y = 45 },
            Open = true,
            IdCreator = c.Id,
            Checkpoints = { cp1, cp2, cp3 },
        };

        //HTTPHelper.Send(c);
        Cookie auth = null;
        yield return HTTPHelper.Instance.AuthLogin("autremil@cndi.fkr", "pass", value => auth = value);
        Debug.Log(auth.Value);
        string decoded = JWTHelper.DecodePayload(auth.Value);
        Debug.Log(decoded);

        Account pasAccount = null;
        yield return HTTPHelper.Instance.GetAccount(auth, value => pasAccount = value);


        //HTTPHelper.Persist(c);
        bool result;
        yield return HTTPHelper.Instance.Persist(q1, auth, value => result = value);

        //tuser.text = JSONHelper.ToJsonString(q1);

        string questJson =
            "{" +
                "\"_idCreator\": \"0@bob\"," +
                "\"geolocalisation\": {" +
                    "\"x\": 10.0," +
                    "\"y\": 45.0" +
                "}," +
                "\"title\": \"quete1\"," +
                "\"description\": \"Super quête cool\"," +
                "\"checkpoints\": [" +
                "{" +
                  "\"text\": \"Text of checkpoint 1\"," +
                  "\"choices\": [" +
                    "\"choix 1\"," +
                    "\"choix 2\"," +
                    "\"choix 3\"" +
                  "]," +
                  "\"answer\": \"choix 1\"," +
                  "\"difficulty\": 0" +
                "}," +
                "{" +
                  "\"text\": \"Text of checkpoint 2\"," +
                  "\"choices\": [" +
                    "\"choix 1\"," +
                    "\"choix 2\"," +
                    "\"choix 3\"" +
                  "]," +
                  "\"answer\": \"choix 3\"," +
                  "\"difficulty\": 0" +
                "}" +
              "]," +
              "\"value\": 0" + //complètement random
            "}";
           
        string accountJson = "{" +
            "\"connection\": {" +
                "\"email\": \"test@mail.com\"," +
                "\"password\": \"test\"" +
            "}," +
            "\"userInformation\": {" +
                "\"lastname\": \"test\"," +
                "\"firstname\": \"test\"," +
                "\"username\": \"test\"," +
                "\"accountType\": \"CREATOR\"" +
            "}," +
            "\"createdAt\": \"2018-04-30T15:52:28\"," +
            "\"updatedAt\": \"2018-04-30T15:52:28\"," +
            "\"game\": {" +
                "\"badges\": [0, 1, 2]," +
                "\"quests\": [{" +
                    "\"_idQuest\": 0," +
                    "\"state\": \"IN_PROGRESS\"," +
                    "\"stats\": { \"earnedXP\": 42 }" +
                "}]," +
                "\"xp\": 42," +
                "\"elapsedTime\": 42" +
            "}" +
            "}";

        string badgeJson = "{" +
            "\"name\": \"test\"," +
            "\"description\": \"description du badge\"," +
            "\"iconPath\": \"path de l'icône\"," +
            "\"require\": {" +
                "\"xp\": 42," +
                "\"totalElapsedTime\": 42" +
            "}," +
            "\"earn\": 42" +
            "}";
        TestJsonToQuest(questJson);
        TestJsonToAccount(accountJson);
    }

    public void TestJsonToQuest(string json)
    {
        Quest quest = JSONHelper.ToQuest(json);
        Assert.AreEqual("0@bob", quest.IdCreator);
        Assert.AreEqual(10.0, quest.Geolocalisation.x);
        Assert.AreEqual(45.0, quest.Geolocalisation.y);
        Assert.AreEqual("quete1", quest.Title);
        Assert.AreEqual("Super quête cool", quest.Description);
        Assert.AreEqual("Text of checkpoint 1", quest.Checkpoints[0].Text);
        Assert.AreEqual("choix 2", quest.Checkpoints[0].Choices[1]);
        Assert.AreEqual("choix 3",quest.Checkpoints[1].Answer);
        Assert.AreEqual(0, quest.Checkpoints[1].Difficulty);
        Assert.AreEqual(0, quest.Value);
        //tuser.text = quest.Title;
    }
    
    public void TestJsonToAccount(string json)
    {
        Account account = null;
        JSONHelper.Instance.ToAccount(json, value => account = value);
        Assert.AreEqual("test@mail.com", account.Mail);
        Assert.AreEqual("test", account.Password);
        Assert.AreEqual("test", account.LastName);
        Assert.AreEqual("test", account.FirstName);
        Assert.AreEqual("test", account.Username);
        Assert.AreEqual(RoleAccount.CREATOR, account.Role);
        Assert.AreEqual("2018-04-30T15:52:28", account.CreationDate.ToString("s"));
        Assert.AreEqual("2018-04-30T15:52:28", account.UpdateDate.ToString("s"));
        //Assert.AreEqual(1, account.Badges[1].Id);
        //Assert.AreEqual(0, account.Quests.ElementAt(0).Key);
        Assert.AreEqual(42, account.ElapsedTime);
        Assert.AreEqual(42, account.Xp);
    }
}

