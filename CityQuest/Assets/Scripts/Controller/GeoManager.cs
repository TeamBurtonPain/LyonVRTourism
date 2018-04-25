using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GeoManager : MonoBehaviour
{

    public Text positionText;
    public Text resultText;
    public float radius;

    protected static GeoManager instance;

    private Coordinates userPosition;
    private Coordinates targetPosition;

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

        positionText.text = "";
        resultText.text = "";

        userPosition = new Coordinates();

        //target = Marseille
        targetPosition = new Coordinates();
        targetPosition.x = 43.296482f;
        targetPosition.y = 5.36978f;

        Start();

        InvokeRepeating("Tester", 10, 5);

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
                //InvokeRepeating("RetrieveGPSData",0,3);
                //Invoke("RetrieveGPSData", 0);
            }
        }
        else
        {
            positionText.text = "GPS not available";
        }
#endif
    }

    void RetrieveGPSData()
    {
        LocationInfo currentGPSPosition = Input.location.lastData;
        userPosition.x = currentGPSPosition.latitude;
        userPosition.y = currentGPSPosition.longitude;
        //string gpsString = userPosition.x + " / " + userPosition.y;
        //string gpsString = currentGPSPosition.latitude + " / " + currentGPSPosition.longitude;
        //positionText.text = gpsString;
    }

    private void Tester()
    {
        IsUserNear(targetPosition, radius);
    }

    private bool IsUserNear(Coordinates target, float radius)
    {
        bool isNear = false;

        userPosition.x = Input.location.lastData.latitude;
        userPosition.y = Input.location.lastData.longitude;

        //positionText.text += "lat =" + userPosition.x + ",  long =" + userPosition.y + "||";

        float distance = Distance(userPosition, target, radius);
        positionText.text = "distance = " + distance;
        if (distance <= radius)
        {
            isNear = true;
        }

        resultText.text += "result = " + isNear + "||";

        return isNear;
    }

    private float Distance(Coordinates coord1, Coordinates coord2, float radius)
    {
        float coord1Lat = coord1.x * Mathf.PI / 180;
        float coord1Long = coord1.y * Mathf.PI / 180;

        float coord2Lat = coord2.x * Mathf.PI / 180;
        float coord2Long = coord2.y * Mathf.PI / 180;

        float R = 6371f;

        float distance = R * Mathf.Acos(Mathf.Cos(coord1Lat) * Mathf.Cos(coord2Lat) *
            Mathf.Cos(coord2Long - coord1Long) + Mathf.Sin(coord1Lat) * Mathf.Sin(coord2Lat));

        return distance;
    }


    IEnumerator rechargeLocation()
    {
        yield return new WaitForSeconds(5.0f);
    }
}
