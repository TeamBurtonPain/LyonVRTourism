using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoc : MonoBehaviour {

    public MapLocalizer localizer;

    public Transform radius;

    void Start () {
        InvokeRepeating("CheckLoca", 1, 1);
        localizer.Scale(radius, GeoManager.Instance.radius); // scale radius circle.
        CheckLoca();
        CenterCam();
    }
	

   private void CheckLoca()
    {
        if (GeoManager.Instance.IsLoaded())
        {
            Vector2 loca = GeoManager.Instance.GetUserPosition();
            localizer.Localise(this.transform, loca.x, loca.y); // place player poit
            localizer.Localise(radius, loca.x, loca.y); // place radius circle
        }
    }

    public void CenterCam()
    {
        Camera.main.transform.position = new Vector3(this.transform.position.x, Camera.main.transform.position.y, this.transform.position.z);
    }
}
