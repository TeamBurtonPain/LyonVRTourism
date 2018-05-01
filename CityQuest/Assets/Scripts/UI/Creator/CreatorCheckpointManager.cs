using System;
using UnityEngine;
using UnityEngine.UI;

public class CreatorCheckpointManager : MonoBehaviour
{
    public InputField EnigmaInputField;
    private int index;
    private string answer;
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

    public string Answer
    {
        get
        {
            return answer;
        }
    }

    public void SetIndex(int i, CreatorMainSceneManager m)
    {
        parent = m;
        index = i;
    }

    public void Btn_CreateNewCheckpoint()
    {
        if (FirstAnswerToggle.isOn && !SecondAnswerToggle.isOn && !ThirdAnswerToggle.isOn)
        {
            answer = FirstAnswerInputField.text;
            parent.AddNewCheckpoint(index+1);
            
        }else if (!FirstAnswerToggle.isOn && SecondAnswerToggle.isOn && !ThirdAnswerToggle.isOn)
        {
            answer = SecondAnswerInputField.text;
            parent.AddNewCheckpoint(index + 1);
        }
        else if (!FirstAnswerToggle.isOn && !SecondAnswerToggle.isOn && ThirdAnswerToggle.isOn)
        {
            answer = ThirdAnswerInputField.text;
            parent.AddNewCheckpoint(index + 1);
        }
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