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

        Coordinates coordinates = new Coordinates();
        coordinates.x = 42.3245f;
        coordinates.y = 4.56978f;
        Quest quest = new Quest(coordinates,"Trouver les pandas",
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
    }

    public void SelectionQuestInHistoric()
    {
        currentState.SelectionQuestInHistoricAction();
    }

    public void StartQuest()
    {
        if (selectedQuest != null && user != null)
        {
            user.AddQuest(selectedQuest);
            currentState.StartQuestAction();
        }
        else
        {
            //TODO : Gestion des erreurs en cas de quest ou de user null
        }
    }

 

    /*********** FIN BOUTONS ***********/

    public static Controller Instance
    {
        get { return instance; }
    }

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