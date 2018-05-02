using System.Collections.Generic;
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

    private IState currentState;
    public IState mapState;
    public IState editorState;
    public IState historicState;
    public IState questState;
    public IState loginState;
    private ConnexionState currentConnexion;

    public bool leavingWindowOpen = false;
    public bool cancelWindowOpen = false;

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
        editorState = new EditorState();
        historicState = new HistoricState();
        questState = new QuestState();
        loginState = new LoginState();

        currentState = loginState;

        //------ Test sample ---------
        Coordinates coordinates = new Coordinates
        {
            x = 42.3245f,
            y = 4.56978f
        };
        Coordinates coordinates2 = new Coordinates
        {
            x = 45.781732f,
            y = 4.872846f
        };
        Coordinates coordinates3 = new Coordinates
        {
            x = 45.771732f,
            y = 4.872846f
        };
        Coordinates coordinates4 = new Coordinates
        {
            x = 45.761732f,
            y = 4.872846f
        };
        Coordinates coordinates5 = new Coordinates
        {
            x = 45.751732f,
            y = 4.872846f
        };
        Coordinates coordinates6 = new Coordinates
        {
            x = 45.741732f,
            y = 4.872846f
        };
        Coordinates coordinates7 = new Coordinates
        {
            x = 45.731732f,
            y = 4.872846f
        };
        Coordinates coordinates8 = new Coordinates
        {
            x = 45.7821732f,
            y = 4.872846f
        };

        Creator creator = new Creator
        {
            FirstName = "John"
        };
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
        Quest q3 = new Quest(coordinates3, "a", "b", 3L, creator, checkpoints);
        Quest q4 = new Quest(coordinates4, "a", "b", 3L, creator, checkpoints);
        Quest q5 = new Quest(coordinates5, "a", "b", 3L, creator, checkpoints);
        Quest q6 = new Quest(coordinates6, "a", "b", 3L, creator, checkpoints);
        Quest q7 = new Quest(coordinates7, "a", "b", 3L, creator, checkpoints);
        Quest q8 = new Quest(coordinates8, "a", "b", 3L, creator, checkpoints);
        existingQuests = new List<Quest>
        {
            quest,
            quest2,
            q3,
            q4,
            q5,
            q6,
            q7,
            q8
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

    //////////////// Pop-up windows
    // Leave
    public void AskLeave()
    {
        if (!leavingWindowOpen)
        {
            leavingWindowOpen = true;
            MNPopup p = new MNPopup("Quitter", "Voulez-vous quitter l'application ?");
            p.AddAction("Oui", () => { Leave(); });
            p.AddAction("Non", () => { leavingWindowOpen = false; });
            p.AddDismissListener(() => { leavingWindowOpen = false; });
            p.Show();
        }
    }

    public void Leave()
    {
        // Chose one of the 2 following (sometime it bugs on some systems)

        //Application.Quit();
        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }

    // BackToMap
    public void AskBackToMap()
    {
        if (!cancelWindowOpen)
        {
            cancelWindowOpen = true;
            MNPopup p = new MNPopup("Retour", "Voulez-vous retourner à la carte ?");
            p.AddAction("Oui", () => { leavingWindowOpen = false; BackToMap(); });
            p.AddAction("Non", () => { leavingWindowOpen = false; });
            p.AddDismissListener(() => { leavingWindowOpen = false; });
            p.Show();
        }
    }

    public void BackToMap()
    {
        LoadMap();
    }
    //////////////////////////
    // end of pop-up windows
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
            if (GeoManager.Instance.IsUserNear(selectedQuest.Geolocalisation))
            {
                user.AddQuest(selectedQuest);
                currentState = questState;
                SceneManager.LoadScene("GameImageScene");
            }
            else{
                Error("Vous êtes trop loin pour lancer cette quête.");
            }
        }
        else
        {
            //TODO : Gestion des erreurs en cas de quest ou de user null
        }
    }

    public void LoadMap()
    {
        currentState = mapState;
        SceneManager.LoadScene("MapScene");
    }
    public void LoadInscription()
    {
        currentState = loginState;
        SceneManager.LoadScene("AccountCreation");
    }
    public void LoadConnexion()
    {
        currentState = loginState;
        SceneManager.LoadScene("Connexion");
    }
    public void LoadUsername()
    {
        currentState = loginState;
        SceneManager.LoadScene("Pseudo");
    }

    public void SelectMenuNewQuest()
    {
        currentState = editorState;
        SceneManager.LoadScene("CreatorMainScene");
    }
    public void SelectMenuMap()
    {
        LoadMap();
    }
    public void SelectMenuHistoric()
    {
        currentState = historicState;
        SceneManager.LoadScene("MyQuests");
    }

    public void SelectMenuSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void SelectMenuLogout()
    {
        // TODO deco en local (persistance)
        currentState = loginState;
        SceneManager.LoadScene("Login");
    }

    public void CreateNewAccount(string firstName, string lastname, string mail, string password, string username)
    {

        // TODO integrity check

        // récupérer les infos locales si elles existent.
        // on se dit que s'il y a un pseudo en local, on le remplace par celui rentré ici de toutes façon. Le reste est gardé.

        // TODO : persistance en ligne + locale de la connexion.

        // if persistance ok -> user = user
        LoadMap();

        // else 
        // Error(message);

    }

    public void CreateNewQuest(string firstName, string lastname, string mail, string password, string username, List<CheckPoint> checkPoints)
    {
        // TODO integrity check
        // Vérifier bon format des données

        // TODO : persistance en ligne de la quête créé si non deja existante.

        // if persistance ok
        LoadMap();

        // else 
        // Error(message);

    }

 

    public void ChooseUsername(string pseudo)
    {
        // TODO des trucs avec ce pseudo
        // persistance local
        LoadMap();
    }

    public void TryConnection(string mail, string pwd)
    {
        // TODO le back.
        // connexion au serveur.
        // if connexion ok 
        //     faire la persistance locale de la connexion au compte +
        //     user = charger l'user depuis la bdd
        LoadMap();
        // else 
        // Error("Aucune correspondance trouvée.");

    }

    /*********** FIN BOUTONS ***********/

    public void Error(string msg)
    {
        MNPopup p = new MNPopup("Erreur", msg);
        p.AddAction("Ok", () => { });
        p.Show();

    }

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