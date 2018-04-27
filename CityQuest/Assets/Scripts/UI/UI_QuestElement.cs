using UnityEngine;
using UnityEngine.UI;

public class UI_QuestElement : MonoBehaviour
{

    private Quest myQuest;
    public Text questNameText;
    public Text questProgressText;

    public void LinkQuest(StateQuest sQuest)
    {
        myQuest = sQuest.Quest;
        questNameText.text = sQuest.Quest.Title;
        questProgressText.text = sQuest.Score.ToString();
    }

    public void SelectQuestInHistoric()
    {
        Controller.Instance.SelectionQuestInHistoric(myQuest);
    }
}
