using System.Collections;
using UnityEngine;

/// <summary>
/// Class managing all the methods needed for the geolocalisation
/// </summary>
public class GeoManager : MonoBehaviour
{

    public float radius = 1;

    protected bool loaded = false;
    protected bool failed = false;

    protected static GeoManager instance;
    public static GeoManager Instance
    {
        get { return instance; }
    }


    /// <summary>
    /// Method initializing the GeoManager instance
    /// </summary>
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
    }

    /// <summary>
    /// Initializes the position input from the device used by the App
    /// </summary>
    void Start()
    {

#if PC
        Debug.Log("On PC / Don't have GPS");
#elif !PC
        StartCoroutine(Init());
#endif
    }


    public IEnumerator Init()
    {
        failed = false;
        //Starting the Location service before querying location
        Input.location.Start(0.5f); // Accuracy of 0.5 m

        int wait = 10; // Per default

        // Checks if the GPS is enabled by the user (-> Allow location )
        if (Input.location.isEnabledByUser)
        {
            while (Input.location.status == LocationServiceStatus.Initializing && wait > 0)
            {
                yield return new WaitForSeconds(1);
                wait--;
            }


            if (Input.location.status == LocationServiceStatus.Failed)
            {
                failed = true;
                yield break;
            }
            else
            {
                loaded = true;
            }
        }
        else
        {
            failed = true;
            yield break;
        }
    }


    public bool IsLoaded()
    {
        return loaded;
    }
    public bool HasFailed()
    {
        return failed;
    }
    /// <summary>
    /// Method that states if a user is near to a location within a perimeter stated in the params. The user coordinates are automatically collected using the device sensors
    /// </summary>
    /// <param name="target">Represents the location targeted by the user</param>
    /// <returns> A boolean that represents the validation of the user's presence nearby the location targeted.</returns>
    public bool IsUserNear(Coordinates target)
    {
        Coordinates userPosition = new Coordinates
        {
            x = Input.location.lastData.latitude,
            y = Input.location.lastData.longitude
        };

        float distance = Distance(userPosition, target);

        return distance <= radius;
    }
    public Vector2 GetUserPosition()
    {
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
}
