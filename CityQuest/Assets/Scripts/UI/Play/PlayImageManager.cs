using UnityEngine;
using UnityEngine.UI;
using System;

class PlayImageManager : MonoBehaviour
{
    public Image researchImage;

    private void Start()
    {
        //researchImage.sprite = Resources.Load<Sprite>(PlayQuestController.Instance.CurrentCheckpoint.Checkpoint.Picture);
        string picture = PlayQuestController.Instance.CurrentCheckpoint.Checkpoint.Picture;
        Char delimiter = ',';
        String[] substrings = picture.Split(delimiter);
        byte[] img;
        try
        {
            img = JSONHelper.FromBase64(substrings[1]);
        }
        catch (Exception e)
        {
            Debug.Log("Erreur lors de la récupération de l'image");
            img = JSONHelper.FromBase64(picture);
        }
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(img);
        researchImage.material = new Material(researchImage.material);
        researchImage.material.mainTexture = tex;
    }
}

