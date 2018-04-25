using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestsListManager : MonoBehaviour {

    public Transform questTemplate;
    public Transform parent;
    private List<Transform> listElements;

    private void Start()
    {
        FillQuestsList(new User("test"));
    }

    public void FillQuestsList(User user)
    {
        int i = 0;
        do
        {
            Transform temp = Instantiate(questTemplate, this.parent);
            temp.GetChild(1).gameObject.GetComponent<Text>().text =  /*user.Quests[i].Quest.Title*/""+ i;
            listElements.Add(temp);
            i++;
        }while (i < user.Quests.Count) ;

        
    }
}
