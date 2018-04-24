using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoManager : MonoBehaviour {

    public GeoManager instance;

    //private Coordonates userLocation;
    //private Coordonates targetLocation;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        /*
        userLocation = new LocationInfo();
        userLocation.
        userLocation.latitude = 48.856614f;
        userLocation.longitude = 2.3522219f;

        targetLocation = new LocationInfo();
        targetLocation.latitude = ;
        targetLocation.longitude = ;
        */
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    bool IsNear (LocationInfo user, LocationInfo target, float radius)
    {
        bool isNear = false;

        float userLat = user.latitude * Mathf.PI / 180;
        float userLong = user.longitude * Mathf.PI / 180;

        float targetLat = target.latitude * Mathf.PI / 180;
        float targetLong = target.longitude * Mathf.PI / 180;

        float R = 6371f;

        float distance = R * Mathf.Acos(Mathf.Cos(userLat) * Mathf.Cos(targetLat) * 
            Mathf.Cos(targetLong - userLong) + Mathf.Sin(userLat) * Mathf.Sin(targetLat));

        if(distance <= radius)
        {
            isNear = true;
        }

        return isNear;
    }

    IEnumerator rechargeLocation()
    {
        yield return new WaitForSeconds(5.0f);
    }
}
