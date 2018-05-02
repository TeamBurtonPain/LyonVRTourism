using UnityEngine;

public class QuestsListManager : MonoBehaviour {

    public UI_QuestElement questTemplate;
    public Transform parent;

    private void Start()
    {
        if (Controller.Instance.User != null && Controller.Instance.User.Quests.Count != 0)
        {
            FillQuestsList(Controller.Instance.User);
        }
        else
        {
            //Error("Allez dans \"Voir la carte\" afin de faire votre première quête");
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
            temp.LinkQuest(stateQuest);
        }
    }
}
