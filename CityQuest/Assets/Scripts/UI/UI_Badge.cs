using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Badge : MonoBehaviour {

    public Image icon;
    public Text title;

    public void LinkBadge(Badge badge)
    {
        //this.icon = badge.iconPath
        this.title.text = badge.Name;
    }
}
