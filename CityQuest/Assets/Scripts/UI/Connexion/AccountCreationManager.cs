using UnityEngine;
using UnityEngine.UI;

public class AccountCreationManager : MonoBehaviour
{
    public InputField firstNameInputField;
    public InputField lastNameInputField;
    public InputField mailInputField;
    public InputField passwordInputField;
    public InputField usernameInputField;
    /*
    public Dropdown YearsDropdown;
    public Dropdown MonthsDropdown;
    public Dropdown DaysDropdown;
    */
    public Toggle aggrement;

    public void Btn_CreateNewAccount()
    {
        if (aggrement.isOn)
        {
            Controller.Instance.CreateNewAccount(firstNameInputField.text, lastNameInputField.text, mailInputField.text, passwordInputField.text, usernameInputField.text);
        }
        else
        {
            Controller.Instance.Error("Vous devez accepter les conditions et tout");
        }
    }

}