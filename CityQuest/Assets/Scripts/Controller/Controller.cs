﻿using System.Collections.Generic;
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
    public GameObject leavingWindow;

    protected static Controller instance;

    private IState currentState;
    public IState mapState;
    public IState historicState;
    public IState questState;
    public IState loginState;
    private ConnexionState currentConnexion;

    private List<Quest> existingQuests;

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

        mapState = new MapState();
        historicState = new HistoricState();
        questState = new QuestState();
        loginState = new LoginState();

        currentState = loginState;

        //------ Test sample ---------
        Coordinates coordinates = new Coordinates();
        coordinates.x = 42.3245f;
        coordinates.y = 4.56978f;
        Coordinates coordinates2 = new Coordinates();
        coordinates2.x = 45.781732f;
        coordinates2.y = 4.872846f;

        Creator creator = new Creator();
        creator.FirstName = "John";
        List<string> choices = new List<string>();
        choices.Add("a");
        choices.Add("b");
        choices.Add("c");
        CheckPoint cp1 = new CheckPoint("pic1.png", "blablablaTextCP1", choices, "b");
        CheckPoint cp2 = new CheckPoint("pic2.png", "blablablaTextCP2", choices, "a");
        List<CheckPoint> checkpoints = new List<CheckPoint>
        {
            cp1,
            cp2
        };
        Quest quest = new Quest(coordinates, "Trouver les pandas roux", "Description des pandas roux", 3L, creator, checkpoints);
        Quest quest2 = new Quest(coordinates2, "Trouver les pandas roux2", "Description des pandas roux2", 3L, creator, checkpoints);
        existingQuests = new List<Quest>
        {
            quest,
            quest2
        };

        user = new User();
        user.AddQuest(quest);
        user.AddQuest(quest2);
        //------ End Test sample -------

        selectedQuest = quest;
        currentConnexion = ConnexionState.DISCONNECTED;
    }

    /// <summary>
    /// Transitions to the specified state s
    /// </summary>
    /// <param name="s">The state.</param>
    //TODO : voir quoi faire d'autre pour effectuer la transition 
    public void Transition(IState s)
    {
        currentState = s;
    }

    void OnApplicationPause(bool pause)
    {
        if (pause && Application.platform == RuntimePlatform.Android)
        {
            // TODO mettre en pause plutot genre retourner sur la scene d' accueil
            Leave();
        }
    }

    public void AskLeave()
    {
        leavingWindow.SetActive(true);
    }

    public void Leave()
    {
        // Chose one of the 2 following (sometime it bugs on some systems)
        //Application.Quit();
        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }

    public void CancelLeave()
    {
        leavingWindow.SetActive(false);
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            currentState.ReturnAction();
        }
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
        currentState.InscriptionAction();
    }

    public void SelectionQuestInHistoric(Quest myQuest)
    {
        currentState.SelectionQuestInHistoricAction(myQuest);
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

    public void SelectMenuNewQuest()
    {
        currentState = mapState;
        SceneManager.LoadScene("MapScene");
    }
    public void SelectMenuHistoric()
    {
        currentState = historicState;
        SceneManager.LoadScene("MyQuests");
    }

    /*
    public void GoQuest()
    {
        currentState.GoQuestAction();
    }*/

    public void SelectMenuSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void SelectMenuLogout()
    {
        // TODO deco
        currentState = loginState;
        SceneManager.LoadScene("Login");
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

    public List<Quest> ExistingQuests
    {
        get { return existingQuests; }
    }

    public User User
    {
        get { return user; }
        set { user = value; }
    }
}