using UnityEngine;
using System.Collections.Generic;

public class QuestsListManager : MonoBehaviour {

    public UI_QuestElement questTemplate;
    public Transform parent;
    private List<UI_QuestElement> listElements;

    private void Start()
    {
        if (Controller.Instance.User != null && Controller.Instance.User.Quests.Count != 0)
        {
            FillQuestsList(Controller.Instance.User);
        }
        else
        {
            Debug.Log("yolo on a réussi !! ");
        }
        
    }

    public void FillQuestsList(User user)
    {
        foreach(StateQuest stateQuest in user.Quests.Values)
        {
            UI_QuestElement temp = Instantiate(questTemplate, this.parent);
            temp.linkQuest(stateQuest);
            listElements.Add(temp);
        }
    }
}
