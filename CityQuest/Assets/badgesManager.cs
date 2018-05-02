using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badgesManager : MonoBehaviour {

    public UI_Badge template;
    public Transform parent;

    private void Start()
    {
        if (Controller.Instance.User != null && Controller.Instance.User.Badges.Count != 0)
        {
            FillBadgesList(Controller.Instance.User);
        }
        else
        {
            //Error("Allez dans \"Voir la carte\" afin de faire votre première quête");
        }

    }

    public void FillBadgesList(User user)
    {
        foreach (Badge badge in user.Badges)
        {
            UI_Badge temp = Instantiate(template, this.parent);
            temp.LinkBadge(badge);
        }
    }
}
