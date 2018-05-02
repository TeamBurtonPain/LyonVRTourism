using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ImageTargetCreator : MonoBehaviour
{

    public string dataSetName = "AR_DB";

    public string questFilter;

    public DataSet dataSet;
    // Use this for initialization
    void Start () {
	    VuforiaARController.Instance.RegisterVuforiaStartedCallback(LoadDataSet);
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    protected virtual void OnDestroy()
    {
        VuforiaARController.Instance.UnregisterVuforiaStartedCallback(LoadDataSet);
    }

    void LoadDataSet()
    {
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        dataSet = objectTracker.CreateDataSet();

        if (dataSet.Load(dataSetName))
        {
            objectTracker.Stop();  // stop tracker so that we can add new dataset

            if (!objectTracker.ActivateDataSet(dataSet))
            {
                // Note: ImageTracker cannot have more than 100 total targets activated
                Debug.Log("<color=yellow>Failed to Activate DataSet: " + dataSetName + "</color>");
            }

            if (!objectTracker.Start())
            {
                Debug.Log("<color=yellow>Tracker Failed to Start.</color>");
            }
            
            int counter = 0;

            IEnumerable<TrackableBehaviour> tbs = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours();
            foreach (TrackableBehaviour tb in tbs)
            {
                if (tb.name == "New Game Object" && tb.TrackableName.Contains(questFilter))
                {
                    // change generic name to include trackable name
                    tb.gameObject.name = ++counter + ":DynamicImageTarget-" + tb.TrackableName;

                    // add additional script components for trackable
                    tb.gameObject.AddComponent<Detection>();
                    tb.gameObject.AddComponent<TurnOffBehaviour>();

                }
            }

        }
        else
        {
            Debug.LogError("<color=yellow>Failed to load dataset: '" + dataSetName + "'</color>");
        }
    }
}
