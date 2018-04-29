
/// <summary>
/// Interface for the state pattern.
/// Methods to override are common actions methods like the return action.
/// </summary>
public interface IState
{
    void ReturnAction();
    void OptionAction();
    void LoginLocalAction();
    void LoginServerAction();
    void InscriptionAction();
    void SelectionQuestInHistoricAction(Quest myQuest);

    // TODO inutile ici ?
    void StartQuestAction();
}