using UnityEngine;
using UnityEngine.UI;
using System;

class PlayImageManager : MonoBehaviour
{
    //public Image researchImage;

    private void Start()
    {
        //researchImage.sprite = Resources.Load<Sprite>(PlayQuestController.Instance.CurrentCheckpoint.Checkpoint.Picture);
        string picture = PlayQuestController.Instance.CurrentCheckpoint.Checkpoint.Picture;
        Char delimiter = ',';
        String[] substrings = picture.Split(delimiter);
        Debug.Log(substrings[1]);
        byte[] img;
        try
        {
            Debug.Log("on a testé de récupérer l'image ;)");
            img = JSONHelper.FromBase64(substrings[1]);
        }
        catch (Exception e)
        {
            Debug.Log("erreur lors de la récupération de l'image");
            img = JSONHelper.FromBase64(picture);
        }
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(img);
        GetComponent<Image>().material.mainTexture = tex;
    }
}

