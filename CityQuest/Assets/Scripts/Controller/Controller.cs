using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public enum ConnexionState
{
    CONNEXION_LOCAL = 0,
    CONNEXION_SERVER,
    DISCONNECTED
}
public class Controller : MonoBehaviour
{

    protected static Controller instance;

    public GameObject loading;

    private IState currentState;
    public IState loadingState;
    public IState mapState;
    public IState editorState;
    public IState historicState;
    public IState questState;
    public IState loginState;
    //private ConnexionState currentConnexion;

    public bool leavingWindowOpen = false;
    public bool cancelWindowOpen = false;

    private List<Quest> existingQuests;

    private User user;
    private Cookie cookie;

    private Quest selectedQuest;

    private StateQuest currentQuest;

    private bool isLoaded = false;
    private bool hasFailed = false;
    public bool IsLoaded { get { return isLoaded; } }
    public bool HasFailed { get { return hasFailed; } }


    void Awake()
    {
        loading.SetActive(false);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        loadingState = new DefaultState();
        mapState = new MapState();
        editorState = new EditorState();
        historicState = new HistoricState();
        questState = new QuestState();
        loginState = new LoginState();

        currentState = loadingState;

    }
    void Start()
    {
        // a coroutine is a function that might take longer than a frame to execute.
        StartCoroutine(InitQuests());
    }

    public IEnumerator InitQuests()
    {
        // yield return xxx means wait for the fcking end of this function without blocking all the system.
        // btw we have to use a callback tu assignate value with this kind of method.

        SetLoaderCircle(true);
        yield return HTTPHelper.Instance.GetAllQuests(value => existingQuests = value);
        SetLoaderCircle(false);

        if (existingQuests == null)
        {
            hasFailed = true;
            yield break;
        }

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

        choices.Add("Du bambou");
        choices.Add("Des oeufs");
        choices.Add("Des M&M's");
        CheckPoint cp1 = new CheckPoint("TestSprites/panda", "", "Quel est l'aliment principal des pandas roux ? ", choices, "bambou", 2);
        CheckPoint cp2 = new CheckPoint("pic2.png", "", "blablablaTextCP2", choices, "a", 1);
        List<CheckPoint> checkpoints = new List<CheckPoint>
        {
            cp1,
            cp2
        };

        Quest quest = new Quest(coordinates, "Trouver les pandas roux", "Description des pandas roux", 30, creator.Id, checkpoints);
        Quest quest2 = new Quest(coordinates2, "Trouver les pandas roux2", "Description des pandas roux2", 30, creator.Id, checkpoints);
        Quest q3 = new Quest(coordinates3, "a", "b", 30, creator.Id, checkpoints);
        Quest q4 = new Quest(coordinates4, "a", "b", 30, creator.Id, checkpoints);
        Quest q5 = new Quest(coordinates5, "a", "b", 30, creator.Id, checkpoints);
        Quest q6 = new Quest(coordinates6, "a", "b", 30, creator.Id, checkpoints);
        Quest q7 = new Quest(coordinates7, "a", "b", 30, creator.Id, checkpoints);
        Quest q8 = new Quest(coordinates8, "a", "b", 30, creator.Id, checkpoints);
        existingQuests.Add(quest);
        existingQuests.Add(quest2);
        existingQuests.Add(q3);
        existingQuests.Add(q4);
        existingQuests.Add(q5);
        existingQuests.Add(q6);
        existingQuests.Add(q7);
        existingQuests.Add(q8);
        StateQuest playing = new StateQuest(quest);

        user = new User();
        user.AddQuest(quest);
        user.AddQuest(quest2);
        //------ End Test sample -------


        isLoaded = true; 

        selectedQuest = quest;
        currentQuest = playing;
        //currentConnexion = ConnexionState.DISCONNECTED;
    }

    public void SetLoaderCircle(bool isLoading)
    {
        loading.SetActive(isLoading);
    }

    void OnApplicationPause(bool pause)
    {
        if (pause && Application.platform == RuntimePlatform.Android)
        {
            // TODO mettre en pause plutot genre retourner sur la scene d' accueil
            // Leave();
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
        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.A))
        {
            currentState.ReturnAction();
        }
    }

    /*********** BOUTONS ***********/

    public void LoginLocal()
    {
        //currentConnexion = ConnexionState.CONNEXION_LOCAL;
        currentState.LoginLocalAction();
    }

    public void LoginServer()
    {
        //currentConnexion = ConnexionState.CONNEXION_SERVER;
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
            else
            {
                Error("Vous êtes trop loin pour lancer cette quête.");
            }
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
        cookie = new Cookie("auth", "");
        currentState = loginState;
        SceneManager.LoadScene("Pseudo");
    }

    public void SelectMenuNewQuest()
    {
        if (user is Account)
        {
            Account account = user as Account;
            if (account.Role == RoleAccount.ADMIN || account.Role == RoleAccount.CREATOR)
            {
                currentState = editorState;
                SceneManager.LoadScene("CreatorMainScene");
            }
            else
            {
                Error("Vous ne disposez pas des autorisations nécessaires pour passer en mode éditeur.");
            }
        }
        else
        {
            Error("Veuillez vous connecter pour pouvoir accéder à ce service");
        }
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
        //TODO : victor à remplacer
        //SceneManager.LoadScene("Settings");
    }

    public void SelectMenuLogout() {
        StartCoroutine(DoLogOut());
    }

    public IEnumerator DoLogOut()
    {
        if (user is Account)
        {
            yield return HTTPHelper.Instance.AuthLogout(cookie);
        }
        currentState = loginState;
        SceneManager.LoadScene("Login");
    }

    public IEnumerator CreateNewAccount(string firstName, string lastname, string mail, string password, string username)
    {
        bool integrity = true;
        if (firstName == null) { Error("Entrez un prénom valide."); integrity = false; }
        if (lastname == null) { Error("Entrez un nom valide."); integrity = false; }
        if (mail != null)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(mail);
            if (!match.Success)
            {
                Error("Entrez un mail valide.");
                integrity = false;
            }
        }
        else { Error("Entrez un mail valide."); integrity = false; }
        if (password == null) { Error("Entrez un mot de passe valide."); ; integrity = false; }
        if (password == null) { Error("Entrez un nom d'utilisateur valide."); ; integrity = false; }

        if (integrity)
        {
            User user = new User(username);
            Account account = new Account(user, mail, password, firstName, lastname, RoleAccount.USER);

            bool request = false;

            SetLoaderCircle(true);
            yield return HTTPHelper.Instance.Persist(account, value => request = value);
            yield return HTTPHelper.Instance.AuthLogin(mail, password, value => cookie = value);
            SetLoaderCircle(false);

            if (request)
                LoadMap();
            else
                Error("Cette adresse mail est déjà utilisée");
        }
    }

    public IEnumerator CreateNewQuest(Coordinates geolocalisation, string title, string description, int experienceEarned,
         string idCreator, List<CheckPoint> checkpoints)
    {
        bool integrity = true;
        if (geolocalisation == null) { Error("Entrez une géolocalisation valide."); integrity = false; }
        if (title == null) { Error("Entrez un titre valide."); integrity = false; }
        if (description == null) { Error("Entrez une description valide."); integrity = false; }
        if (experienceEarned > 0) { Error("Entrez une valeur d'expérience gagnée valide."); integrity = false; }
        if (checkpoints == null) { Error("Entrez des checkpoints valides."); integrity = false; }

        if (integrity)
        {
            Quest quest = new Quest(geolocalisation, title, description, experienceEarned, idCreator, checkpoints);

            SetLoaderCircle(true);
            bool request = false;
            yield return HTTPHelper.Instance.Persist(quest, this.cookie, value => request = value);
            SetLoaderCircle(false);

            if (request)
                LoadMap();
            else
                Error("Erreur lors de la création de la quête.");
        }
    }



    public void ChooseUsername(string pseudo)
    {
        // TODO des trucs avec ce pseudo
        // persistance local
        LoadMap();
    }

    public IEnumerator TryConnection(string mail, string pwd)
    {
        SetLoaderCircle(true);
        yield return HTTPHelper.Instance.AuthLogin(mail, pwd, value => cookie = value);
        SetLoaderCircle(false);

        if (cookie.Value != "")
        {
            Account a = null;
            yield return HTTPHelper.Instance.GetAccount(cookie, value => a = value);
            if (a.Mail == "")
            {
                Error(a.LastName);
                LoadConnexion();
            }
            else
            {
                user = a;
                // TODO le back.
                LoadMap();
            }

        }
        else
        {
            Error("Aucune correspondance trouvée.");
        }

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

    public StateQuest CurrentQuest
    {
        get { return currentQuest; }
        set { currentQuest = value; }
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