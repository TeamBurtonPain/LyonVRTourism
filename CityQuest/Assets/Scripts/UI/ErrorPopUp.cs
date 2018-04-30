using UnityEngine;
using UnityEngine.UI;

public class ErrorPopUp : MonoBehaviour {
    public Text msg;

    public void SetError(string error)
    {
        msg.text = error;
        this.gameObject.SetActive(true);
    }

    public void Dismiss()
    {
        Destroy(gameObject);
    }

}
