public class MapState : DefaultState
{
    /// <summary>
    /// Pops a pop up to close the app
    /// </summary>
    public override void ReturnAction()
    {
        Controller.Instance.AskLeave();
    }
}