public class EditorState : DefaultState
{
    public override void ReturnAction()
    {
        Controller.Instance.AskBackToMap();
    }
}