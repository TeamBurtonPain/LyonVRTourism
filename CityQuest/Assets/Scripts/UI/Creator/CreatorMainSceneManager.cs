using System;
using System.Collections.Generic;
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
    protected List<CreatorCheckpointManager> myCheckpoints;

    public void Btn_CreateNewQuest()
    {
        this.gameObject.SetActive(false);
        AddNewCheckpoint(0);
        /*
            Controller.Instance.CreateNewQuest(
                QuestNameInputField.text, 
                QuestDescriptionInputField.text,
                QuestValueInputField.text, 
                PositionLatInputField.text, 
                PositionLongInputField.text); 
                */
    }

    public void AddNewCheckpoint(int index)
    {
       // myCheckpoints.Last() set active false si not null
        CreatorCheckpointManager temp = Instantiate(CheckpointTemplate, this.transform);
        temp.SetIndex(index, this);
        myCheckpoints.Add(temp);
    }

    public void Btn_UseCurrentLoc()
    {
        // Find Current location

    }

}