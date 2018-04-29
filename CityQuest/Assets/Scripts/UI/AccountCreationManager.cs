using UnityEngine;
using UnityEngine.UI;

public class AccountCreationManager : MonoBehaviour
{
    public Button CreateAccountButton;
    public InputField FirstNameInputField;
    public InputField LastNameInputField;
    public InputField MailInputField;
    public InputField PasswordInputField;
    public Dropdown YearsDropdown;
    public Dropdown MonthsDropdown;
    public Dropdown DaysDropdown;

    public void Btn_CreateNewAccount()
    {
        // Passer inputs au controleur ou à une fonction autre ? Située ou ? 
        Controller.Instance.CreateNewAccount();
    }

}