using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;


public class QuestState : DefaultState
{
    public QuestState(Controller c) : base(c)
    {
    }

    public new void GoQuestAction()
    {
        //Passage scène quête
     //   SceneManager.LoadScene("QuestScene");
    }

}