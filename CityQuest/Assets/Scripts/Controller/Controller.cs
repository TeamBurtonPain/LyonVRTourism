using System.Collections.Generic;
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
    private State mapState;
    private State historicState;
    private State questState;
    private State loginState;
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

        mapState = new MapState(this);
        historicState = new HistoricState(this);
        questState = new QuestState(this);
        loginState = new LoginState(this);

        currentState = loginState;

        //------ Test sample ---------
        Coordinates coordinates = new Coordinates();
        coordinates.x = 42.3245f;
        coordinates.y = 4.56978f;

        Creator creator = new Creator();
        creator.FirstName = "John";
        List<string> choices = new List<string>();
        choices.Add("a");
        choices.Add("b");
        choices.Add("c");
        CheckPoint cp1 = new CheckPoint("pic1.png","blablablaTextCP1",choices,"b");
        CheckPoint cp2 = new CheckPoint("pic2.png", "blablablaTextCP2", choices, "a");
        List<CheckPoint> checkpoints = new List<CheckPoint>();
        checkpoints.Add(cp1);
        checkpoints.Add(cp2);
        Quest quest = new Quest(coordinates, "Trouver les pandas roux", "Description des pandas roux", 3L, creator, checkpoints);

        user = new User();
        user.AddQuest(quest);
        //------ End Test sample -------

        selectedQuest = quest;
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

    public void SelectMenuNewQuest()
    {
        SceneManager.LoadScene("MapScene");
    }
    public void SelectMenuHistoric()
    {
        SceneManager.LoadScene("MyQuests");
    }

    public void SelectMenuSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void SelectMenuLogout()
    {
        SceneManager.LoadScene("Logout");
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