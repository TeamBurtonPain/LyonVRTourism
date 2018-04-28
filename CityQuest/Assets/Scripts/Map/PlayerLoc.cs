using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoc : MonoBehaviour {

    public MapLocalizer localizer;

    void Start () {
        InvokeRepeating("CheckLoca", 0, 1);
    }
	

   private void CheckLoca()
    {
        if (GeoManager.Instance.IsLoaded())
        {
            Vector2 loca = GeoManager.Instance.GetUserPosition();
            localizer.Localise(this.transform, loca.x, loca.y);
        }
    }

    public void CenterCam()
    {
        Camera.main.transform.position = new Vector3(this.transform.position.x, Camera.main.transform.position.y, this.transform.position.z);
    }
}
