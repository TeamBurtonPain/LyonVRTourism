using UnityEngine;
using UnityEngine.UI;

class PlayQuestionManager : MonoBehaviour
{
    public Text questionText;

    private void Start()
    {
        questionText.text = PlayQuestController.Instance.CurrentCheckpoint.Checkpoint.Text;
    }
}

