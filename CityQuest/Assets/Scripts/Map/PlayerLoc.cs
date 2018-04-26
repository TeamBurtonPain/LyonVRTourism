using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoc : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InvokeRepeating("CheckLoca", 1, 1);
    }
	

   private void CheckLoca()
    {
        if (GeoManager.Instance.IsLoaded())
        {
            Vector2 loca = GeoManager.Instance.GetUserPosition();
            MapLocalizer.Instance.Localise(this.transform, loca.x, loca.y);
        }
    }
}
