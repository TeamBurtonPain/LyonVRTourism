using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileController : MonoBehaviour {

    public Text username;
    public Text experience;


	void Start () {
        username.text = Controller.Instance.User.Username;
        experience.text = Controller.Instance.User.Xp.ToString();
	}
}
