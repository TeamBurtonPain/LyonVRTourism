using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Layout_Manager : MonoBehaviour {

    public Button menu;
    public Color backgroundColor;
    public GameObject layout;
    public GameObject menuDeroulant;

    private void Awake()
    {
        menuDeroulant.SetActive(false);
        ColorManager();
    }

    public void ColorManager()
    {
        backgroundColor = Color.gray;
        layout.GetComponent<Image>().color = backgroundColor;
        menuDeroulant.GetComponent<Image>().color = backgroundColor;
    }

    public void OpenMenu()
    {
        menuDeroulant.SetActive(true);
    }

    public void CloseMenu()
    {
        menuDeroulant.SetActive(false);
    }

    public void MenuDeroulantManager()
    {
        if (menuDeroulant.activeSelf)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }

    public void OnClickMenuNewQuest()
	{
        Controller.Instance.SelectMenuNewQuest();
	}
    public void OnClickMenuHistoric()
    {
        Controller.Instance.SelectMenuHistoric();
    }
    public void OnClickMenuSettings()
    {
        Controller.Instance.SelectMenuSettings();
    }
    public void OnClickMenuLogout()
    {
        Controller.Instance.SelectMenuLogout();
    }

}
