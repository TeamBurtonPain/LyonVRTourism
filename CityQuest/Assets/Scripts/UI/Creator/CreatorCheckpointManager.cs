using System;
using UnityEngine;
using UnityEngine.UI;

public class CreatorCheckpointManager : MonoBehaviour
{
    public InputField EnigmaInputField;
    private int index;
    private CreatorMainSceneManager parent;
    public InputField FirstAnswerInputField;
    public InputField SecondAnswerInputField;
    public InputField ThirdAnswerInputField;
    public Image FurnishedImage;
    public Toggle FirstAnswerToggle;
    public Toggle SecondAnswerToggle;
    public Toggle ThirdAnswerToggle;

    public int Index
    {
        get
        {
            return index;
        }
    }

    public void SetIndex(int i, CreatorMainSceneManager m)
    {
        parent = m;
        index = i;
    }

    public void Btn_CreateNewCheckpoint()
    {
        if (FirstAnswerToggle.isOn && !SecondAnswerToggle.isOn && !ThirdAnswerToggle.isOn ||
            !FirstAnswerToggle.isOn && SecondAnswerToggle.isOn && !ThirdAnswerToggle.isOn ||
            !FirstAnswerToggle.isOn && !SecondAnswerToggle.isOn && ThirdAnswerToggle.isOn)
        {
            parent.AddNewCheckpoint(index+1);

        }
       /* {
            Controller.Instance.CreateNewCheckpoint(
                EnigmaInputField.text,
                FirstAnswerInputField.text,
                SecondAnswerInputField.text,
                ThirdAnswerInputField.text,
                FurnishedImage.ToString(),
                //Convert.ToBase64String(FurnishedImage), // Pass image as string 
                FirstAnswerInputField.text);
        }
        else if (!FirstAnswerToggle.isOn && SecondAnswerToggle.isOn && !ThirdAnswerToggle.isOn)

        {
            Controller.Instance.CreateNewCheckpoint(
                EnigmaInputField.text,
                FirstAnswerInputField.text,
                SecondAnswerInputField.text,
                ThirdAnswerInputField.text,
                FurnishedImage.ToString(),
                //Convert.ToBase64String(FurnishedImage), // Pass image as string 
                SecondAnswerInputField.text);
        }

        else if (!FirstAnswerToggle.isOn && !SecondAnswerToggle.isOn && ThirdAnswerToggle.isOn)

        {
            Controller.Instance.CreateNewCheckpoint(
                EnigmaInputField.text,
                FirstAnswerInputField.text,
                SecondAnswerInputField.text,
                ThirdAnswerInputField.text,
                FurnishedImage.ToString(),
                //Convert.ToBase64String(FurnishedImage), // Pass image as string 
                ThirdAnswerInputField.text);
        }*/
        else
        {
            Controller.Instance.Error("Vous devez sélectionner une unique solution !");
        }
    }

    public void Btn_CreateQuest()
    {
        parent.ValidateQuest();

    }

}