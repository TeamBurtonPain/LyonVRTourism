using UnityEngine;
using UnityEngine.UI;

public class Layout_Manager : MonoBehaviour {

    public Button menu;
    public Color backgroundColor;
    public GameObject layout;
    public GameObject menuBackground;
    public GameObject menuDeroulant;

    /// <summary>
    /// Hide the burger menu when the scene is loaded, and set both layout and menu colors the same
    /// </summary>
    private void Awake()
    {
        menuBackground.SetActive(false);
        menuDeroulant.SetActive(false);
        ColorManage();
    }

    /// <summary>
    /// Setup the background color of both menu and layout
    /// </summary>
    public void ColorManage()
    {
        backgroundColor = Color.gray;
        layout.GetComponent<Image>().color = backgroundColor;
        menuDeroulant.GetComponent<Image>().color = backgroundColor;
    }

    public void OpenMenu()
    {
        menuBackground.SetActive(true);
        menuDeroulant.SetActive(true);
    }

    public void CloseMenu()
    {
        menuBackground.SetActive(false);
        menuDeroulant.SetActive(false);
    }

    /// <summary>
    /// Handle the Hiding and Showing of the burger menu
    /// </summary>
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

}
