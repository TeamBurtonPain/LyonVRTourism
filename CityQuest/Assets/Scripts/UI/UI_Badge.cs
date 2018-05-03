using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Badge : MonoBehaviour {

    //public Image icon;
    public Text title;

    public void LinkBadge(Badge badge)
    {
        string picture = badge.IconPath;
        byte[] img = JSONHelper.FromBase64(picture);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(img);
        GetComponent<Image>().material.mainTexture = tex;
        //this.icon = badge.iconPath
        this.title.text = badge.Name;
    }
}
