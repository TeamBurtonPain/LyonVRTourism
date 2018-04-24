using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Model;
using UnityEngine;

public class User
{
    private string id;
    private string username;
    private long xp;
    private List<Badge> badges;
    private Dictionary<long, StateQuest> quests;

}
