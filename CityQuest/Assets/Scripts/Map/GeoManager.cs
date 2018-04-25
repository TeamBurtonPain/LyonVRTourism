using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GeoManager : MonoBehaviour
{

    public Text x;
    public Text resultText;
    public Text positionText;
    public float radius = 1;

    protected bool loaded = false;
    protected bool failed = false;

    protected static GeoManager instance;
    public static GeoManager Instance
    {
        get { return instance; }
    }

    private Coordinates userPosition;
    private Coordinates targetPosition;// A retirer plus tard (test V1)

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

        positionText.text = "";// A retirer plus tard (test V1)
        resultText.text = "";// A retirer plus tard (test V1)

        userPosition = new Coordinates();

        //Example : target = Marseille --> A retirer plus tard (test V1)
        targetPosition = new Coordinates();
        targetPosition.x = 43.296482f;
        targetPosition.y = 5.36978f;

        //InvokeRepeating("Tester", 10, 5);// A retirer plus tard (test V1)

    }

    /// <summary>
    /// Initializes the position input from the device used by the App
    /// </summary>
    void Start()
    {

#if PC
        Debug.Log("On PC / Don't have GPS");
#elif !PC
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
                failed = true;
                //latitude and longitude equals to 0
            }
            else
            {
                loaded = true;
                // We start the timer to check each tick (every 3 sec) the current gps position
                //InvokeRepeating("RetrieveGPSData",0,3); // A retirer plus tard (test V1)
                //Invoke("RetrieveGPSData", 0); // A retirer plus tard (test V1)
            }
        }
        else
        {
            positionText.text = "GPS not available";
            failed = true;
        }
#endif
    }

    public bool IsLoaded()
    {
        return loaded;
    }
    public bool HasFailed()
    {
        return failed;
    }

    // A retirer plus tard (test V1)
    private void Tester()
    {
        IsUserNear(targetPosition, radius);
    }

    /// <summary>
    /// Method that states if a user is near to a location within a perimeter stated in the params. The user coordinates are automatically collected using the device sensors
    /// </summary>
    /// <param name="target">Represents the location targeted by the user</param>
    /// <param name="radius">Parameter that represents the minimum perimeter in which the user must be to begin its quest. radius in kilometers.</param>
    /// <returns> A boolean that represents the validation of the user's presence nearby the location targeted.</returns>
    public bool IsUserNear(Coordinates target, float radius)
    {
        bool isNear = false;

        userPosition.x = Input.location.lastData.latitude;
        userPosition.y = Input.location.lastData.longitude;

        positionText.text = "lat =" + userPosition.x + ",  long =" + userPosition.y;

        float distance = Distance(userPosition, target);

        if (distance <= radius)
        {
            isNear = true;
        }

        resultText.text = "result = " + isNear;

        return isNear;
    }
    public Vector2 GetUserPosition()
    {
        resultText.text = Input.location.lastData.latitude + "/" + Input.location.lastData.longitude;
        return new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
    }

    /// <summary>
    /// Method calculating the euclidian distance between to coordinates using spherical consideration (with the earth radius R)
    /// </summary>
    /// <param name="coord1">Represents one geographical position to study</param>
    /// <param name="coord2">Represents the other geographical position to study</param>
    /// <returns>Returns a float representing the euclidian distance between the two position studied</returns>
    private float Distance(Coordinates coord1, Coordinates coord2)
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

    /// <summary>
    /// Sets the targetPosition attribute from the GeoManager
    /// </summary>
    /// <param name="latitude">Latitude of the target</param>
    /// <param name="longitude">Longitude of the target</param>
    public void SetTargetPosition(float latitude,float longitude)
    {
        targetPosition.x = latitude;
        targetPosition.y = longitude;
    }

    /// <summary>
    /// Gets the targetPosition attribute from the GeoManager
    /// </summary>
    /// <returns>Returns the Coordinates object that represents the target's position</returns>
    public Coordinates GetTargetPosition()
    {
        return targetPosition;
    }
}
