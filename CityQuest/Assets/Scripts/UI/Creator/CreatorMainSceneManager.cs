using UnityEngine;
using UnityEngine.UI;

public class CreatorMainSceneManager : MonoBehaviour
{
    public InputField PositionLongInputField;
    public InputField PositionLatInputField;
    public InputField QuestNameInputField;
    public InputField QuestDescriptionInputField;
    public InputField QuestValueInputField;
 

    public void Btn_CreateNewQuest()
    {
        //if (nom quête existe déjà || Valeurs entrées =/= int)
      //  {
            Controller.Instance.CreateNewQuest(
                QuestNameInputField.text, 
                QuestDescriptionInputField.text,
                QuestValueInputField.text, // Vérifier que c'est un int 
                PositionLatInputField.text, // Vérifier que c'est un int 
                PositionLongInputField.text); // Vérifier que c'est un int 
      //  }
      //else
      //  {
      //    Controller.Instance.Error("Une quête de même nom est déjà existante");
      //}
    }

}