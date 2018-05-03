using UnityEngine;
using UnityEngine.UI;

public class ConnexionManager : MonoBehaviour {

    public InputField inputMail;
    public InputField inputPassword;

    public void TryConnection()
    {
        StartCoroutine(Controller.Instance.TryConnection(inputMail.text, inputPassword.text));
    }


}
