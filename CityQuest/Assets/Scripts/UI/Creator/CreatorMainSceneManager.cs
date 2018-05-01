using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreatorMainSceneManager : MonoBehaviour
{
    public InputField PositionLongInputField;
    public InputField PositionLatInputField;
    public CreatorCheckpointManager CheckpointTemplate;
    public InputField QuestNameInputField;
    public InputField QuestDescriptionInputField;
    public InputField QuestValueInputField;
    protected List<CreatorCheckpointManager> AllCheckpoints;

    public void Btn_StartNewQuest()
    {
        this.gameObject.SetActive(false);
        AddNewCheckpoint(0);
        
    }

    public void AddNewCheckpoint(int index)
    {
        if (AllCheckpoints.LastOrDefault() != null)
        {
            AllCheckpoints.Last().gameObject.SetActive(false);
        }
        CreatorCheckpointManager temp = Instantiate(CheckpointTemplate, this.transform);
        temp.SetIndex(index, this);
        AllCheckpoints.Add(temp);
    }

    public void Btn_UseCurrentLoc()
    {
        // Find Current location

    }

    public void ValidateQuest()
    {
        Controller.Instance.CreateNewQuest(
            QuestNameInputField.text,
            QuestDescriptionInputField.text,
            QuestValueInputField.text,
            PositionLatInputField.text,
            PositionLongInputField.text,
            ToCheckPoints()); // Pass checkpoints
    }

    public List<CheckPoint> ToCheckPoints()
    {
        List<CheckPoint> MyCheckPoints = new List<CheckPoint>();
        foreach (CreatorCheckpointManager creatorCheckpoint in AllCheckpoints)
        {
            List<string> choices = new List<string>();
            choices.Add(creatorCheckpoint.FirstAnswerInputField.text);
            choices.Add(creatorCheckpoint.SecondAnswerInputField.text);
            choices.Add(creatorCheckpoint.ThirdAnswerInputField.text);

            CheckPoint temp = new CheckPoint(
                creatorCheckpoint.FurnishedImage.ToString(), // Pass image as string 
                creatorCheckpoint.EnigmaInputField.text,
                choices,
                creatorCheckpoint.Answer);  
            MyCheckPoints.Add(temp);
        }

        return MyCheckPoints;
    }

}