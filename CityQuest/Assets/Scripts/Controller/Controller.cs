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
    public IState aRCameraState;
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
        aRCameraState = new ARCameraState();

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

        isLoaded = true; 
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
        StartCoroutine(TryStartQuest());
    }

    public IEnumerator TryStartQuest()
    {
        if (selectedQuest != null && user != null)
        {
            if (GeoManager.Instance.IsUserNear(selectedQuest.Geolocalisation))
            {

                Quest fetchedQuest = null;

                SetLoaderCircle(true);
                yield return HTTPHelper.Instance.GetQuest(selectedQuest.Id, value => fetchedQuest = value);
                SetLoaderCircle(false);

                selectedQuest = fetchedQuest;

                user.AddQuest(selectedQuest);
                currentQuest = user.Quests[selectedQuest.Id];
                currentState = questState;
                SceneManager.LoadScene("GameImageScene");
            }
            else
            {
                Error("Vous êtes trop loin pour lancer cette quête.");
            }
        }
    }

    public void ReStartQuest()
    {
        StartCoroutine(TryReStartQuest());
    }

    public IEnumerator TryReStartQuest()
    {
        if (selectedQuest != null && user != null)
        {
            if (GeoManager.Instance.IsUserNear(selectedQuest.Geolocalisation))
            {

                Quest fetchedQuest = null;

                SetLoaderCircle(true);
                yield return HTTPHelper.Instance.GetQuest(selectedQuest.Id, value => fetchedQuest = value);
                SetLoaderCircle(false);


                selectedQuest = fetchedQuest;

                user.Quests[fetchedQuest.Id] = new StateQuest(selectedQuest);
                currentQuest = user.Quests[fetchedQuest.Id];
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
    public void LoadVuforia()
    {
        currentState = aRCameraState;
    }
    public void QuitVuforia()
    {
        currentState = questState;
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
        SceneManager.LoadScene("Profile");
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
        user = new User(pseudo);
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

            SetLoaderCircle(true);
            yield return HTTPHelper.Instance.GetAccount(cookie, value => a = value);
            SetLoaderCircle(false);

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

    public void PersistUserAdvancement()
    {
        if(user is Account)
        {
            StartCoroutine(TryPersistUser((Account)user));
        }
    }

    public IEnumerator TryPersistUser(Account a)
    {
        yield return HTTPHelper.Instance.UpdateData(a, cookie);
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