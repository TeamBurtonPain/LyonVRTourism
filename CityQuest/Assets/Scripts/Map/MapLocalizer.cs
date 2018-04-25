using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocalizer : MonoBehaviour {
    public BingMapsTexture mainPlan;

    protected static MapLocalizer instance;
    public static MapLocalizer Instance
    {
        get { return instance; }
    }

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

    public void Localise(Transform t, float lat, float longi)
    {
        float sizeCoef = 10f;
        float latOffset = - (mainPlan.origin.x - lat) / mainPlan.size.x +  0.5f; // is between -0.5 and 0.5
        float longOffset = - (mainPlan.origin.y - longi) / mainPlan.size.y - 0.5f; // is between -0.5 and 0.5

        t.transform.position = mainPlan.transform.position + sizeCoef * new Vector3(longOffset, 0, latOffset) ;
    }
}
