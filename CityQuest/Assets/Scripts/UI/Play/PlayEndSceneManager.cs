using UnityEngine;
using UnityEngine.UI;

class PlayEndSceneManager : MonoBehaviour
{

    public Text FinishedText;
    public Image ScoreBar;
    public Text DurationText;
    public GameObject BadgePanel;

    private void Start()
    {
        FinishedText.text = "Vous avez terminé la quête " + PlayQuestController.Instance.CurrentQuest.Quest.Title + " !";

        // Durée
        int hour = (int) Mathf.Round((float)PlayQuestController.Instance.CurrentQuest.TimeElapsed / 3600);
        int min = (int)Mathf.Round(((float)PlayQuestController.Instance.CurrentQuest.TimeElapsed % 3600) / 60);
        DurationText.text = "";
        if (hour != 0)
        {
            DurationText.text = hour + "h et ";
        }
        DurationText.text += min + "min";

        // Score
        ScoreBar.fillAmount = (float)PlayQuestController.Instance.CurrentQuest.Score / PlayQuestController.Instance.GetScoreMax() ;

        // Badges
    }
}
