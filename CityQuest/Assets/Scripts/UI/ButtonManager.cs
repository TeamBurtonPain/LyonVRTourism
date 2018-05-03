using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void Btn_SelectMenuNewQuest()
    {
        Controller.Instance.SelectMenuNewQuest();
    }
    public void Btn_SelectMenuMap()
    {
        Controller.Instance.SelectMenuMap();
    }
    public void Btn_BackToMap()
    {
        Controller.Instance.AskBackToMap();
    }
    public void Btn_SelectMenuHistoric()
    {
        Controller.Instance.SelectMenuHistoric();
    }
    public void Btn_SelectMenuSettings()
    {
        Controller.Instance.SelectMenuSettings();
    }
    public void Btn_LogOut()
    {
        Controller.Instance.SelectMenuLogout();
    }
    public void Btn_AskLeave()
    {
        Controller.Instance.AskLeave();
    }

    public void Btn_LoginLocal()
    {
        Controller.Instance.LoginLocal();
    }

    public void Btn_LoginServer()
    {
        Controller.Instance.LoginServer();
    }

    public void Btn_Inscription()
    {
        Controller.Instance.Inscription();
    }
}