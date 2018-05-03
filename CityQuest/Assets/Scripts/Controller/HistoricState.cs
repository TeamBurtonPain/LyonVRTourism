using UnityEngine.SceneManagement;

public class HistoricState : DefaultState
{
    public override void SelectionQuestInHistoricAction(Quest quest)
    {
        Controller.Instance.SelectedQuest = quest;
        Controller.Instance.LoadMap();
    }
    public override void ReturnAction()
    {
        Controller.Instance.LoadMap();
    }


}