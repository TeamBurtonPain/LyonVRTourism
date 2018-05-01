using System;
using UnityEngine;
using UnityEngine.UI;

public class CreatorCheckpointManager : MonoBehaviour
{
    public Text indexText;
    public InputField enigmaInputField;

    public InputField firstAnswerInputField;
    public InputField secondAnswerInputField;
    public InputField thirdAnswerInputField;

    public Image furnishedImage;

    public Toggle firstAnswerToggle;
    public Toggle secondAnswerToggle;
    public Toggle thirdAnswerToggle;

    private CreatorMainSceneManager parent;

    private int index;
    private string answer;

    public int Index
    { get {return index;} }

    public string Answer
    { get{ return answer; } }

    public void SetIndex(int i, CreatorMainSceneManager m)
    {
        parent = m;
        index = i;
        indexText.text = "n. " + (index + 1);
    }

    public void SelectFirst()
    {
        if (firstAnswerToggle.isOn)
        {
            answer = firstAnswerInputField.text;
            secondAnswerToggle.isOn = false;
            thirdAnswerToggle.isOn = false;
        }
    }
    public void SelectSecond()
    {
        if (secondAnswerToggle.isOn)
        {
            answer = firstAnswerInputField.text;
            firstAnswerToggle.isOn = false;
            thirdAnswerToggle.isOn = false;
        }
    }
    public void SelectThird()
    {
        if (thirdAnswerToggle.isOn)
        {
            answer = firstAnswerInputField.text;
            firstAnswerToggle.isOn = false;
            secondAnswerToggle.isOn = false;
        }
    }

    public void Btn_CreateNewCheckpoint()
    {
        if(!firstAnswerToggle.isOn && !firstAnswerToggle.isOn && !firstAnswerToggle.isOn)
        {
            Controller.Instance.Error("Veuillez selectionner une bonne réponse");
        }
        else
        {
            parent.AddNewCheckpoint(index + 1);
        }
    }

    public void Btn_CreateQuest()
    {
        parent.ValidateQuest();
    }

}