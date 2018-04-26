public class MapState : DefaultState
{
    public MapState(Controller c) : base(c)
    {
    }

    /// <summary>
    /// Pops a pop up to close the app
    /// </summary>
    public override void ReturnAction()
    {
        controller.AskLeave();
    }

    public new void OptionAction()
    {
    }
}