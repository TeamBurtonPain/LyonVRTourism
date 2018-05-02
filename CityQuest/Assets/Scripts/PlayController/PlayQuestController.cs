using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

class PlayQuestController : MonoBehaviour
{
    protected static PlayQuestController instance;

    private StateQuest currentQuest;
    private StateCheckPoint currentCheckpoint;
    private int questProgress;
    private int checkpointProgress;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        currentQuest = Controller.Instance.CurrentQuest;
        questProgress = CheckQuestProgress();
        currentCheckpoint = currentQuest.Checkpoints[questProgress];
        checkpointProgress = 0;
    }

    public int CheckQuestProgress()
    {
        int progress = 0;
        foreach(StateCheckPoint checkpoint in currentQuest.Checkpoints)
        {
            if(checkpoint.Status == StatusCheckPoint.FINISHED)
            {
                progress++;
            } else
            {
                break;
            }
        }
        return progress;
    }

    public bool CheckAnswer(string answer)
    {
        if(answer.ToLower().Contains(currentCheckpoint.Checkpoint.Answer.ToLower()))
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void GoNextScene()
    {
        Debug.Log("etat = " + checkpointProgress);
        switch (checkpointProgress)
        {
            case 0:
                GoToQuestion();
                break;
            case 1:
                GoToInformations();
                break;
            case 2:
                GoToNextCheckpoint();
                break;
            default:
                Debug.LogError("checkpointProgress="+ checkpointProgress + ", error : not comprised between 0 and 2");
                break;
        }
        checkpointProgress = (checkpointProgress + 1)%3;
    }

    private void GoToNextCheckpoint()
    {
        currentQuest.Checkpoints[questProgress].Status = StatusCheckPoint.FINISHED;
        if(questProgress < currentQuest.Checkpoints.Count-1)
        {
            questProgress++;
            currentCheckpoint = currentQuest.Checkpoints[questProgress];
            SceneManager.LoadScene("GameImageScene");
        } else
        {
            Debug.Log("Fini!");
            // TODO : Scene de fin
            // TODO : Detruire controller !!!
        }
    }

    private void GoToQuestion()
    {
        if (currentQuest.Checkpoints[questProgress].Checkpoint.Choices.Count == 0)
            SceneManager.LoadScene("GameQuestion");
        else
            SceneManager.LoadScene("GameQuestionMulti");
    }

    private void GoToInformations()
    {
        SceneManager.LoadScene("GameInformations");
    }

    public void SkipCheckpoint()
    {
        questProgress++;
        checkpointProgress = 0;
    }

    public void OpenCamera()
    {
        SceneManager.LoadScene("ImageRecognition");
    }

    public static PlayQuestController Instance
    {
        get { return instance; }
    }

    public StateCheckPoint CurrentCheckpoint
    {
        get { return currentCheckpoint; }
        set { currentCheckpoint = value; }
    }

    public StateQuest CurrentQuest
    {
        get { return currentQuest; }
        set { currentQuest = value; }
    }
}
