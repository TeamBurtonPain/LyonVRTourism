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
    public UI_Badge_Icon badgeIcon;
    public Transform parent;

    public void LinkQuest(StateQuest sQuest)
    {
        myQuest = sQuest.Quest;
        questNameText.text = sQuest.Quest.Title;
        questProgressText.text = sQuest.Score.ToString();
        circularBar.fillAmount = (float) sQuest.Score;
        badges = Controller.Instance.User.Badges;
        // ATTENTION CA NE DEVRAIT PAS DEPENDRE DU USER MAIS DE LA QUETE
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
        //ATTENTION CA DEVRAIT DEPENDRE DE LA QUETE ET PAS DU USER
        foreach (Badge badge in user.Badges)
        {
            UI_Badge_Icon temp = Instantiate(badgeIcon, this.parent);
            temp.LinkBadge(badge);
        }
    }

    public void SelectQuestInHistoric()
    {
        Controller.Instance.SelectionQuestInHistoric(myQuest);
    }
}
