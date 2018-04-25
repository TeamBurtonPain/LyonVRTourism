using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 
/// </summary>
/// <seealso cref="State" />
public class DefaultState : State
{
    private Controller controller;

    public DefaultState(Controller c)
    {
        controller = c;
    }

    /// <summary>
    /// Returns to the map page
    /// </summary>
    /// <seealso cref="MapState" />
    public void ReturnAction()
    {
        controller.Transition(new MapState(controller));
    }

    public void OptionAction()
    {
    }

    public void LoginLocalAction() { }
    public void LoginServerAction() { }
    public void InscriptionAction() { }
    public void SelectionQuestInHistoricAction() { }
}