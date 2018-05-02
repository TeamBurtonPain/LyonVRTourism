using UnityEngine;

public class PlayButtonManager : MonoBehaviour
{
    public void Btn_GoNextScene()
    {
        PlayQuestController.Instance.GoNextScene();
    }

    public void Btn_SkipCheckpoint()
    {
        PlayQuestController.Instance.SkipCheckpoint();
    }

    public void Btn_OpenCamera()
    {
        PlayQuestController.Instance.OpenCamera();
    }
}
