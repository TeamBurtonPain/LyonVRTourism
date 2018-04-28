using UnityEngine.SceneManagement;

public class HistoricState : DefaultState
{
    public override void SelectionQuestInHistoricAction(Quest quest)
    {
        Controller.Instance.SelectedQuest = quest;
        SceneManager.LoadScene("MapScene");
    }

}