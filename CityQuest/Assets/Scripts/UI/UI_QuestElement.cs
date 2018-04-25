using UnityEngine;
using UnityEngine.UI;

public class UI_QuestElement : MonoBehaviour
{
    private Quest myQuest;
    public Text t;
    public Text s;

    public void linkQuest(StateQuest sQuest)
    {
        myQuest = sQuest.Quest;
        t.text = sQuest.Quest.Title;
        s.text = sQuest.Score.ToString();
    }

    public void Call()
    {
        //Controller.Instance.Methode(myQuest.id);
    }
}
