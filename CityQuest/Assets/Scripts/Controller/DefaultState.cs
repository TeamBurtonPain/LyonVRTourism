﻿using UnityEngine;
using UnityEngine.SceneManagement;


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

    public void OptionAction() { }

    public virtual void LoginLocalAction() {Debug.Log("Le mauvais"); }
    public void LoginServerAction() { }
    public void InscriptionAction() { }
    public void SelectionQuestInHistoricAction() { }
    public void StartQuestAction() { }

    
    
}