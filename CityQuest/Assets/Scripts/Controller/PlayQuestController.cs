using UnityEngine;
using System.Collections.Generic;

class PlayQuestController : MonoBehaviour
{
    protected static PlayQuestController instance;

    private StateQuest currentQuest;
    private StateCheckPoint currentCheckpoint;

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

        // Tests
        Coordinates coordinates = new Coordinates();
        coordinates.x = 42.3245f;
        coordinates.y = 4.56978f;
        Coordinates coordinates2 = new Coordinates();
        coordinates2.x = 45.781732f;
        coordinates2.y = 4.872846f;

        Creator creator = new Creator();
        creator.FirstName = "John";
        List<string> choices = new List<string>();
        choices.Add("Du bambou");
        choices.Add("Des oeufs");
        choices.Add("Des M&M's");
        CheckPoint cp1 = new CheckPoint("pic1.png", "Quel est l'aliment principal des pandas roux ? ", choices, "Du bambou");
        CheckPoint cp2 = new CheckPoint("pic2.png", "blablablaTextCP2", choices, "a");
        List<CheckPoint> checkpoints = new List<CheckPoint>
        {
            cp1,
            cp2
        };
        Quest quest = new Quest(coordinates, "Trouver les pandas roux", "Description des pandas roux", 3L, creator, checkpoints);
        Quest quest2 = new Quest(coordinates2, "Trouver les pandas roux2", "Description des pandas roux2", 3L, creator, checkpoints);
        StateQuest playing = new StateQuest(quest);
        // End tests

        currentQuest = playing;
        currentCheckpoint = playing.Checkpoints[0];
    }

    public bool CheckAnswer(string answer)
    {
        if(answer.Contains(currentCheckpoint.Checkpoint.Answer))
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
