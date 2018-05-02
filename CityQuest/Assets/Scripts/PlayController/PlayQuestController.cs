using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

class PlayQuestController : MonoBehaviour
{
    protected static PlayQuestController instance;

    private StateQuest currentQuest;
    private StateCheckPoint currentCheckpoint;
    private int questProgress;

    void Awake()
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
        
        // TODO : Vérifier la validité de la réponse
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
