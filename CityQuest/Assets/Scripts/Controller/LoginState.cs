using UnityEngine.SceneManagement;

public class LoginState : DefaultState
{
    public override void LoginLocalAction()
    {
        //TODO : Penser à la récupération du User stocké en local
        //TODO : Code de gestion de première utilisation
        Controller.Instance.LoadMap();
    }

    public override void LoginServerAction()
    {
        //TODO : Code d'authentification au serveur
        Controller.Instance.LoadMap();
    }

    public override void InscriptionAction()
    {
        //TODO : Code d'inscription
        Controller.Instance.LoadMap();
    }

}