using UnityEngine;
using UnityEngine.UI;

public class CheckpointMultiAnsManager : MonoBehaviour
{

    public UI_AnswerElement answerTemplate;
    public Transform parent;
    public Button touchToNext;
    public Transform buttonParent;

    private void Start()
    {
        if (PlayQuestController.Instance.CurrentCheckpoint != null)
        {
            FillAnswersList(PlayQuestController.Instance.CurrentCheckpoint.Checkpoint);
        }
        else
        {
            //TODO : Set a default error message
        }

    }

    public void FillAnswersList(CheckPoint checkpoint)
    {
        foreach (string text in checkpoint.Choices)
        {
            UI_AnswerElement temp = Instantiate(answerTemplate, this.parent);
            temp.LinkAnswer(text, touchToNext, buttonParent);
        }
    }
}
