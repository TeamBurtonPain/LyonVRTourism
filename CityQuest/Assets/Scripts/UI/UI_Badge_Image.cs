using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UI_Badge_Image : MonoBehaviour {

    public void LinkImage(Badge badge)
    {
        string picture = badge.IconPath;
        Char delimiter = ',';
        String[] substrings = picture.Split(delimiter);
        Debug.Log(substrings[1]);
        byte[] img;
        try
        {
            img = JSONHelper.FromBase64(substrings[1]);
        }
        catch (Exception e)
        {
            Debug.Log("c'est la merde si tu passes pas la dedans");
            img = JSONHelper.FromBase64(picture);
        }
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(img);
        GetComponent<Image>().material = new Material(GetComponent<Image>().material);
        GetComponent<Image>().material.mainTexture = tex;

    }
}
