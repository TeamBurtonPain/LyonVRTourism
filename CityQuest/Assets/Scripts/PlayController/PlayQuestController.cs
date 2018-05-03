using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

class PlayQuestController : MonoBehaviour
{
    protected static PlayQuestController instance;
    protected static float coef = 10;

    private StateQuest currentQuest;
    private StateCheckPoint currentCheckpoint;
    private int questProgress;
    private int checkpointProgress;
    private DateTime checkpointStartTime;
    private bool isGood = false;

    private Boolean destroyOnLoad;

    void Start()
    {
        if (instance == null)
        {
            instance = this;

            currentQuest = Controller.Instance.CurrentQuest;
            questProgress = CheckQuestProgress();
            currentCheckpoint = currentQuest.Checkpoints[questProgress];
            checkpointProgress = 0;
            checkpointStartTime = System.DateTime.Now;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        
        DontDestroyOnLoad(gameObject);

        
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (destroyOnLoad) Destroy(gameObject);
        destroyOnLoad = true;
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
        if (answer.ToLower().Contains(currentCheckpoint.Checkpoint.Answer.ToLower()))
        {
        } else
        {
            isGood = false;
        }
        return isGood;
    }

    public void GoNextScene()
    {
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
        currentQuest.Checkpoints[questProgress].TimeElapsed = System.DateTime.Now.Subtract(checkpointStartTime).TotalSeconds;
        currentQuest.Checkpoints[questProgress].Status = StatusCheckPoint.FINISHED;
        if(PlayQuestController.Instance.CurrentCheckpoint.Checkpoint.Badge != null)
        {
            Controller.Instance.User.Badges.Add(PlayQuestController.Instance.CurrentCheckpoint.Checkpoint.Badge);
        }
        if(questProgress < currentQuest.Checkpoints.Count-1)
        {
            questProgress++;
            currentCheckpoint = currentQuest.Checkpoints[questProgress];
            checkpointStartTime = System.DateTime.Now;
            destroyOnLoad = false;
            SceneManager.LoadScene("GameImageScene");
        } else
        {
            currentQuest.TimeElapsed = 0;
            foreach (StateCheckPoint checkpoint in currentQuest.Checkpoints)
            {
                currentQuest.TimeElapsed += checkpoint.TimeElapsed;
            }
            currentQuest.Done = true;
            destroyOnLoad = false;
            SceneManager.LoadScene("EndQuestScene");
        }
        Controller.Instance.PersistUserAdvancement();
    }

    private void IncrementeScore()
    {
        currentQuest.Score += CalculateScore(currentCheckpoint.Checkpoint, isGood);
    }

    private float CalculateScore(CheckPoint c, bool isSuccess)
    {
        if (isSuccess)
        {
            return c.Difficulty * coef;
        }
        else
        {
            return 0;
        }
    }


    private void GoToQuestion()
    {
        Controller.Instance.QuitVuforia();
        if (currentQuest.Checkpoints[questProgress].Checkpoint.Choices.Count == 0)
        {
            destroyOnLoad = false;
            SceneManager.LoadScene("GameQuestion");
        } else
        {
            destroyOnLoad = false;
            SceneManager.LoadScene("GameQuestionMulti");
        }
    }

    private void GoToInformations()
    {
        destroyOnLoad = false;
        SceneManager.LoadScene("GameInformations");
    }

    public void SkipCheckpoint()
    {
        //TODO : gérer le score?
        checkpointProgress = 1;
        GoToQuestion();
    }

    public void OpenCamera()
    {
        Controller.Instance.LoadVuforia();
        destroyOnLoad = false;
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

    public float GetScoreMax()
    {
        float total = 0;
        foreach( StateCheckPoint c in currentQuest.Checkpoints)
        {
            total += CalculateScore(c.Checkpoint, true);
        }
        return total;
    }
}
