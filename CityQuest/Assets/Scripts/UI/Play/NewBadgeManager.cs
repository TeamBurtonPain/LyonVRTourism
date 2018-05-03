using UnityEngine;
using UnityEngine.UI;

class NewBadgeManager : MonoBehaviour
{
    public Text BadgeName;
    public Image BadgeImage;

    
    public void GetBadge(Badge badge)
    {
        BadgeName.text = badge.Name;
        // Appeler le serveur pour récupérer le badge correspondant à idBadge
        // Remplacer BadgeName.text par le nom du badge
        // Afficher l'image du badge sur BadgeImage
    }

}

