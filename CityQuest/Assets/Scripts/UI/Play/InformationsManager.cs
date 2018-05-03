using UnityEngine;
using UnityEngine.UI;

class InformationsManager : MonoBehaviour
{
    public Text InformationText;
    public Transform Parent;
    public Canvas NewBadgePanel;

    void Start()
    {
        InformationText.text = PlayQuestController.Instance.CurrentCheckpoint.Checkpoint.Text;
    }
    public void Btn_Next ()
    {
        if(PlayQuestController.Instance.CurrentCheckpoint.Checkpoint.IdBadge != null)
        {
           Instantiate(NewBadgePanel, this.Parent);
        } else
         {
            PlayQuestController.Instance.GoNextScene();
        }
    }
}

