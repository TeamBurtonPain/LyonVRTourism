public class QuestState : DefaultState
{
    public override void ReturnAction()
    {
        Controller.Instance.AskBackToMap();
    }
}