using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;


public class HistoricState : DefaultState
{
    public HistoricState(Controller c) : base(c)
    {
    }

    public new void SelectionQuestInHistoricAction()
    {
        SceneManager.LoadScene("MapScene");
    }

}