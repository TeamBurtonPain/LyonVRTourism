using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GeoManager : MonoBehaviour
{

    public Text t; 
    protected static GeoManager instance;

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

        t.text = "nifrebkbdskf";


        Start();
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



    
    void Start()
    {

#if PC
        Debug.Log("On PC / Don't have GPS");
#elif !PC
        bool gpsInit = false;
        //Starting the Location service before querying location
        Input.location.Start(0.5f); // Accuracy of 0.5 m

        int wait = 1000; // Per default

        // Checks if the GPS is enabled by the user (-> Allow location )
        if (Input.location.isEnabledByUser)
        {
            while (Input.location.status == LocationServiceStatus.Initializing && wait > 0)
            {
                wait--;
            }


            if (Input.location.status == LocationServiceStatus.Failed)
            {

            }
            else
            {
                gpsInit = true;
                // We start the timer to check each tick (every 3 sec) the current gps position
                InvokeRepeating("RetrieveGPSData",0,3);
            }
        }
        else
        {
            t.text = "GPS not available";
        }
#endif
    }

    void RetrieveGPSData()
    {
        LocationInfo currentGPSPosition = Input.location.lastData;
        string gpsString = currentGPSPosition.latitude + " / " + currentGPSPosition.longitude;
        t.text = gpsString;
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
