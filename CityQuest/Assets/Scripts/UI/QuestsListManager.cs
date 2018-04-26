using UnityEngine;
using System.Collections.Generic;

public class QuestsListManager : MonoBehaviour {

    public UI_QuestElement questTemplate;
    public Transform parent;
    // private List<UI_QuestElement> listElements;

    private void Start()
    {
        if (Controller.Instance.User != null && Controller.Instance.User.Quests.Count != 0)
        {
            FillQuestsList(Controller.Instance.User);
        }
        else
        {
            //TODO : Set a default error message that say or redirect to the Mapscene so the user actually does his first quest
        }
        
    }

    /// <summary>
    /// Creates selectable components for each quest started or finished by the current user, and insert them into the Historic scene (also known as the "MyQuests" scene
    /// </summary>
    /// <param name="user">The current user</param>
    public void FillQuestsList(User user)
    {
        foreach(StateQuest stateQuest in user.Quests.Values)
        {
            UI_QuestElement temp = Instantiate(questTemplate, this.parent);
            temp.linkQuest(stateQuest);
           // listElements.Add(temp);
        }
    }
}
