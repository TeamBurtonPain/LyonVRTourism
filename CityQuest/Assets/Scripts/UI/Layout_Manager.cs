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

    public void SelectButtonMesQuetesManager()
    {
        SceneManager.LoadScene("Mes quêtes");
    }
    public void SelectButtonParametresManager()
    {
        SceneManager.LoadScene("Paramètres");
    }
}
