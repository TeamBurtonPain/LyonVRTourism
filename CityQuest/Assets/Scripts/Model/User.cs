using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class User
{
    private static long instanceCounter = 0;
    private string id;
    private string username;
    private long xp;
    private List<Badge> badges;


    /// <summary>
    /// The Status of the quests began by the user.
    /// The key is the ID of the quest, the value is the state associated to this quest.
    /// </summary>
    private Dictionary<long, StateQuest> quests;

    public User(string name)
    {
        username = name;
        id = instanceCounter++.ToString() + "@" + username;
        xp = 0;
        badges = new List<Badge>();
        quests = new Dictionary<long, StateQuest>();
    }

    internal User(string name, string id, long xp, List<Badge> badges, Dictionary<long, StateQuest> quests)
    {
        this.username = name;
        this.id = id;
        this.xp = xp;
        this.badges = badges;
        this.quests = quests;
    }

    public string Id
    {
        get { return id; }
    }

    public string Username
    {
        get { return username; }
    }

    public long Xp
    {
        get { return xp; }
    }

    public List<Badge> Badges
    {
        get { return badges; }
        set { badges = value; }
    }

    public Dictionary<long, StateQuest> Quests
    {
        get { return quests; }
        set { quests = value; }
    }
}