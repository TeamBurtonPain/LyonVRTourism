using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Model;
using System.Collections.Generic;

public class QuestsListManager : MonoBehaviour {

    public Transform questTemplate;
    public Transform parent;
    private List<Transform> listElements;

    //A ENLEVER
    private void Start()
    {
        FillQuestsList(new User("test"));
    }
    //----------------------------------------

    public void FillQuestsList(User user)
    {
        foreach(System.Collections.Generic.KeyValuePair<long,StateQuest> quest in user.Quests)
        {
            Transform temp = Instantiate(questTemplate, this.parent);
            //temp.GetChild(0).gameObject.GetComponent<Text>().text = quest.Value.Quest.
            listElements.Add(temp);
        }

        
    }
}
