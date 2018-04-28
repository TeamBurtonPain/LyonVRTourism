﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocalizer : MonoBehaviour {
    public BingMapsTexture mainPlan;

    public void Localise(Transform t, float lat, float longi)
    {
        float sinLatitude = Mathf.Sin(lat * Mathf.PI / 180.0f);

        float pixelX = (((longi + 180) / 360) * 256 * Mathf.Pow(2, mainPlan.initialZoom + 1));
        float pixelY = ((0.5f - Mathf.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Mathf.PI)) * 256 * Mathf.Pow(2, mainPlan.initialZoom + 1));

        float pixelOffsetX = pixelX/256f - Mathf.FloorToInt(pixelX / 256);
        float pixelOffsetY = pixelY/256f - Mathf.FloorToInt(pixelY / 256);

        float latOffset = - pixelOffsetY + 0.5f; // is between -0.5 and 0.5
        float longOffset = pixelOffsetX - 0.5f; // is between -0.5 and 0.5
        
        t.transform.position = mainPlan.transform.position + 10 * mainPlan.transform.lossyScale.x * new Vector3(longOffset, 0, latOffset) ;
    }
   
}
