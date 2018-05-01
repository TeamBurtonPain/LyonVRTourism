using UnityEngine;
using UnityEngine.UI;

class PlayImageManager : MonoBehaviour
{
    public Image researchImage;

    private void Start()
    {
        researchImage.sprite = Resources.Load<Sprite>(PlayQuestController.Instance.CurrentCheckpoint.Checkpoint.Picture);
        //researchImage.sprite = Resources.Load < Sprite > (PlayQuestController.Instance.CurrentCheckpoint.Checkpoint.Picture);
    }
}

