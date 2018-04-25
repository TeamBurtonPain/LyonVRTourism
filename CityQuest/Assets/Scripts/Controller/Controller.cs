using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ConnexionState
{
    CONNEXION_LOCAL = 0,
    CONNEXION_SERVER,
    DISCONNECTED
}

public class Controller : MonoBehaviour
{
    protected static Controller instance;

    private State currentState;
    private ConnexionState currentConnexion;
    private User user;
    private Quest selectedQuest;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        user = null;
        selectedQuest = null;
        currentConnexion = ConnexionState.DISCONNECTED;
    }

    /// <summary>
    /// Transitions to the specified state s
    /// </summary>
    /// <param name="s">The state.</param>
    //TODO : voir quoi faire d'autre pour effectuer la transition 
    public void Transition(State s)
    {
        currentState = s;
    }


    /*********** BOUTONS ***********/

    public void LoginLocal()
    {
        currentState.LoginLocalAction();
        currentConnexion = ConnexionState.CONNEXION_LOCAL;
    }

    public void LoginServer()
    {
        currentState.LoginServerAction();
        currentConnexion = ConnexionState.CONNEXION_SERVER;
    }

    public void Inscription()
    {
        currentState.InscriptionAction();
        currentConnexion = ConnexionState.CONNEXION_SERVER;
    }

    public void SelectionQuestInHistoric()
    {
       // selectedQuest = ? Assigner selected quest à quête sélectionnée
        currentState.SelectionQuestInHistoricAction();
    }

    public void StartNewQuest()
    {
    }

    public void GoQuest()
    {
        currentState.GoQuestAction();
    }

    public void SelectionQuestInMap()
    {
       
    }

    public void Menu()
    {
    }

    /*********** FIN BOUTONS ***********/

    public Quest GetSelectedQuest()
    {
        return selectedQuest;
    }

    public void SetSelectedQuest(Quest newSelectedQuest)
    {
        selectedQuest = newSelectedQuest;
    }
}