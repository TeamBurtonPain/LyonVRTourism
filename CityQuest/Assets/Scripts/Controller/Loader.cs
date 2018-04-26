using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Loader : MonoBehaviour {
    
    private IEnumerator Start()
    {
        while (! (GeoManager.Instance.IsLoaded() || GeoManager.Instance.HasFailed()) )
        {
            yield return null;
        }
        if (GeoManager.Instance.HasFailed())
        {
            Debug.Log("Loading failed.");
        }
        else
        {
            SceneManager.LoadScene("Login");
        }
    }
}
