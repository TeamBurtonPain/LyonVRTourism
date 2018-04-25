using UnityEngine;
using UnityEngine.UI;

public class UI_QuestElement : MonoBehaviour
{

    private Quest myQuest;
    public Text questNameText;
    public Text questProgressText;

    public void linkQuest(StateQuest sQuest)
    {
        myQuest = sQuest.Quest;
        questNameText.text = sQuest.Quest.Title;
        questProgressText.text = sQuest.Score.ToString();
    }

    public void SelectQuestInHistoric()
    {
        Controller.Instance.SelectedQuest = myQuest;
        Controller.Instance.SelectionQuestInHistoric();
    }
}
