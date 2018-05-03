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
            // TODO : Afficher le nom du badge
            // TODO : demander au serveur les infos sur le badge
            // TODO : ajouter les infos à l'user
            // TODO : ajouter le badge à la quête
        } else
         {
            PlayQuestController.Instance.GoNextScene();
        }
    }
   
}

