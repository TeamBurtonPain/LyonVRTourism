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
    public List<CreatorCheckpointManager> AllCheckpoints;

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
            AllCheckpoints); // Pass checkpoints
    }

}