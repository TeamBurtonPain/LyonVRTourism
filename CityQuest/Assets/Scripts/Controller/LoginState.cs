using UnityEngine.SceneManagement;

public class LoginState : DefaultState
{
    public override void LoginLocalAction()
    {
        // TODO check is user existe en local
        // if user found en local
        //     Controller.Instance.LoadMap();
        // else 
            Controller.Instance.LoadUsername();
    }

    public override void LoginServerAction()
    {
        Controller.Instance.LoadConnexion();
    }

    public override void InscriptionAction()
    {
        Controller.Instance.LoadInscription();
    }
    public override void ReturnAction()
    {
        //StartCoroutine(Controller.Instance.SelectMenuLogout());
        Controller.Instance.SelectMenuLogout();
    }

}