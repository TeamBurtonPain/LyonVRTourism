using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestElement : MonoBehaviour
{

    private Quest myQuest;
    public Text questNameText;
    public Text questProgressText;
    public List<Badge> badges;
    public Image circularBar;

    //public UI_BadgeElement badgePicture;
    public Transform parent;

    public void LinkQuest(StateQuest sQuest)
    {
        myQuest = sQuest.Quest;
        questNameText.text = sQuest.Quest.Title;
        questProgressText.text = sQuest.Score.ToString();
        circularBar.fillAmount = (float) sQuest.Score;
        badges = Controller.Instance.User.Badges;
        if (Controller.Instance.User != null && Controller.Instance.User.Badges.Count != 0)
        {
            FillBadgesList(Controller.Instance.User);
        }
        else
        {
            // TODO : Do nothing or error message ? 
        }
    }

    public void FillBadgesList(User user)
    {
        foreach (Badge badge in user.Badges)
        {
            //UI_BadgeElement temp = Instantiate(badgePicture, this.parent);
            //temp.LinkBadge( );
        }
    }

    public void SelectQuestInHistoric()
    {
        Controller.Instance.SelectionQuestInHistoric(myQuest);
    }
}
