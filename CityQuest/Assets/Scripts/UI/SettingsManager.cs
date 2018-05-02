using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Text userName;
    public Text xp;
    public Text badges;

    private void Start()
    {
        userName.text = "Nom d'utilisateur : test2"; // Controller.Instance.User.Username;
        xp.text = "Xp Gagné : test1";  // Controller.Instance.User.Xp;
        badges.text = "Mes badges :";
    }

}