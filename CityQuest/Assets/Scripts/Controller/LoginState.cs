using UnityEngine.SceneManagement;

public class LoginState : DefaultState
{
    public LoginState(Controller c) : base(c)
    {
    }

    public override void LoginLocalAction()
    {
        //TODO : Penser à la récupération du User stocké en local
        //TODO : Code de gestion de première utilisation
        SceneManager.LoadScene("MapScene");
    }

    public new void LoginServerAction()
    {
        //TODO : Code d'authentification au serveur
        SceneManager.LoadScene("MapScene");
    }

    public new void InscriptionAction()
    {
        //TODO : Code d'inscription
        SceneManager.LoadScene("MapScene");
    }

}