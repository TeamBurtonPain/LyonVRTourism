public class ARCameraState : DefaultState
{
    public override void ReturnAction()
    {
        Controller.Instance.StartQuest();
    }
}