﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_QuestElement : MonoBehaviour
{

    private Quest myQuest;
    public Text questNameText;
    public Text questProgressText;
    public List<Badge> badges;
    public Image circularBar;
    public UI_Badge_Icon badgeIcon;
    public Transform parent;

    public void LinkQuest(StateQuest sQuest)
    {
        myQuest = sQuest.Quest;
        questNameText.text = sQuest.Quest.Title;
        questProgressText.text = sQuest.Score.ToString();
        circularBar.fillAmount = (float) sQuest.Score;
        badges = Controller.Instance.User.Badges;
        List<CheckPoint> checkpoints = myQuest.Checkpoints;
        FillBadgesList(checkpoints);
    }

    public /*IENumerator*/ void FillBadgesList(List<CheckPoint> checkpoints)
    {
        foreach (CheckPoint checkpoint in checkpoints)
        {
            UI_Badge_Icon temp = Instantiate(badgeIcon, this.parent);
            temp.LinkBadge(checkpoint.Picture);
        }
    }

    public void SelectQuestInHistoric()
    {
        Controller.Instance.SelectionQuestInHistoric(myQuest);
    }
}
