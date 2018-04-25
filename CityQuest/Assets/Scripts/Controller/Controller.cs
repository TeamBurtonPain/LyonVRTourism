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
        currentConnexion = ConnexionState.CONNEXION_LOCAL;
        currentState.LoginLocalAction();
    }

    public void LoginServer()
    {
        currentConnexion = ConnexionState.CONNEXION_SERVER;
        currentState.LoginServerAction();
    }

    public void Inscription()
    {
        currentConnexion = ConnexionState.CONNEXION_SERVER;
        currentState.InscriptionAction();
    }

    public void SelectionQuestInHistoric()
    {
       // selectedQuest = ? Assigner selected quest à quête sélectionnée
        currentState.SelectionQuestInHistoricAction();
    }

    public void StartNewQuest()
    {
        if (selectedQuest != null && user != null)
        {
            StateQuest newStateQuest = new StateQuest(selectedQuest);
            user.Quests.Add(selectedQuest.Id, newStateQuest);
            currentState.StartNewQuestAction();
        }
        else
        {
            //TODO : Gestion des erreurs en cas de quest ou de user null
        }
    }

    public void GoQuest()
    {
        
    }

    public void SelectionQuestInMap()
    {
       
    }

    public void Menu()
    {
    }

    /*********** FIN BOUTONS ***********/
    public Quest SelectedQuest
    {
        get { return selectedQuest; }
        set { selectedQuest = value; }
    }

    public User User
    {
        get { return user; }
        set { user = value; }
    }
}