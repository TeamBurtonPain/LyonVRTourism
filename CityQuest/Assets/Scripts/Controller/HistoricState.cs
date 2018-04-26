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