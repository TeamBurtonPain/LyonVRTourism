using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreatorMainSceneManager : MonoBehaviour
{
    public InputField questNameInputField;

    public InputField positionLatInputField;
    public InputField positionLongInputField;

    public InputField questDescriptionInputField;
    public InputField questValueInputField;

    public CreatorCheckpointManager checkpointTemplate;

    protected List<CreatorCheckpointManager> allCheckpoints = new List<CreatorCheckpointManager>();

    public void Btn_StartNewQuest()
    {
        this.gameObject.SetActive(false);
        AddNewCheckpoint(0);
    }

    public void AddNewCheckpoint(int index)
    {
        if (allCheckpoints.Count != 0)
        {
            allCheckpoints.Last().gameObject.SetActive(false);
        }
        CreatorCheckpointManager temp = Instantiate(checkpointTemplate);
        temp.SetIndex(index, this);
        allCheckpoints.Add(temp);
    }

    public void Btn_UseCurrentLoc()
    {
        if (GeoManager.Instance.IsLoaded())
        {
            Vector2 loca = GeoManager.Instance.GetUserPosition();
            positionLatInputField.text = loca.x.ToString();
            positionLongInputField.text = loca.y.ToString();
        }
    }

    public void ValidateQuest()
    {
        /*
        Coordinates coordinates = new Coordinates(float.Parse(positionLatInputField.text, System.Globalization.CultureInfo.InvariantCulture.NumberFormat),
            float.Parse(positionLongInputField.text, System.Globalization.CultureInfo.InvariantCulture.NumberFormat));
        StartCoroutine(Controller.Instance.CreateNewQuest(
            coordinates,
            questNameInputField.text,
            questDescriptionInputField.text,
            Convert.ToInt32(questValueInputField.text),
            Controller.Instance.User.Id,
            ToCheckPoints())); // Pass checkpoints
            */
        Controller.Instance.LoadMap();
    }

    public List<CheckPoint> ToCheckPoints()
    {
        List<CheckPoint> MyCheckPoints = new List<CheckPoint>();
        foreach (CreatorCheckpointManager creatorCheckpoint in allCheckpoints)
        {
            List<string> choices = new List<string>
            {
                creatorCheckpoint.firstAnswerInputField.text,
                creatorCheckpoint.secondAnswerInputField.text,
                creatorCheckpoint.thirdAnswerInputField.text
            };

            CheckPoint temp = new CheckPoint(
                creatorCheckpoint.furnishedImage.ToString(), // Pass image as string 
                //TODO nom de l'image
                " NOM BUVDUBDIV3YTGUYEVIDFUE A CHANGER",
                creatorCheckpoint.enigmaInputField.text,
                "Question ?",
                choices,
                creatorCheckpoint.Answer,
                4,
                null); 
            MyCheckPoints.Add(temp);
        }

        return MyCheckPoints;
    }

}