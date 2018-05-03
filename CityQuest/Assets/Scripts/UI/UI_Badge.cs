using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Badge : MonoBehaviour {

    public Text title;
    public UI_Badge_Image template;
    public Transform parent;

    public void LinkBadge(Badge badge)
    {
        this.title.text = badge.Name;
        UI_Badge_Image temp = Instantiate(template, this.parent);
        temp.LinkImage(badge);
    }
}
