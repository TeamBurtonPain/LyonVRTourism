using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;


public class LoginState : DefaultState
{
    public LoginState(Controller c) : base(c)
    {
    }

    public new void LoginLocalAction()
    {
        //TODO : Penser à la récupération du User stocké en local
        //Code de gestion de première utilisation
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