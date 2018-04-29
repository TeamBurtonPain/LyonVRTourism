using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameCreationManager : MonoBehaviour {

    public InputField inputUsername;

    public void UsePseudo()
    {
        Controller.Instance.ChooseUsername(inputUsername.text);
    }
}
