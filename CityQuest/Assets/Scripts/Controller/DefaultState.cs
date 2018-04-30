/// <summary>
/// 
/// </summary>
/// <seealso cref="State" />
public class DefaultState : IState
{
    /// <summary>
    /// Returns to the map page
    /// </summary>
    /// <seealso cref="MapState" />
    public virtual void ReturnAction()
    {
        Controller.Instance.Transition(Controller.Instance.mapState);
    }

    public virtual void OptionAction() { }

    public virtual void LoginLocalAction() { }
    public virtual void LoginServerAction() { }
    public virtual void InscriptionAction() { }
    public virtual void SelectionQuestInHistoricAction(Quest myQuest) { }

    //TODO inutile ici ?
    public virtual void StartQuestAction() {

        //Controller.user.AddQuest(selectedQuest);
    }
}