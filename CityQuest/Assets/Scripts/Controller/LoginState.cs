using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginState : DefaultState
{
    public LoginState(Controller c) : base(c)
    {
    }

    public override void LoginLocalAction()
    {
        //TODO : Penser à la récupération du User stocké en local
        //Code de gestion de première utilisation
        Debug.Log("Le bon");
        SceneManager.LoadScene("MapScene");
    }

    public new void LoginServerAction()
    {
        //Code d'authentification au serveur
        SceneManager.LoadScene("MapScene");
    }

    public new void InscriptionAction()
    {
        //Code d'inscription
        SceneManager.LoadScene("MapScene");
    }

}