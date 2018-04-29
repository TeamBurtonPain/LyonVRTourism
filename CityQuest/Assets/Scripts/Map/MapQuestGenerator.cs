using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapQuestGenerator : MonoBehaviour {

    public QuestLoc template;
    public MapLocalizer m;
    public QuestDetails_Manager quest_details_manager;

    private List<QuestLoc> quests;

	// Use this for initialization
	void Start () {
        quests = new List<QuestLoc>();
		foreach(Quest q in Controller.Instance.ExistingQuests)
        {
            QuestLoc temp = Instantiate(template, this.transform);
            temp.LinkQuest(q,m, this);
            quests.Add(temp);
        }
	}

    public void UpdateSelect()
    {
        quest_details_manager.UpdateContent();
        foreach (QuestLoc q in quests)
        {
            q.UpdateSelect();
        }
    }
}
