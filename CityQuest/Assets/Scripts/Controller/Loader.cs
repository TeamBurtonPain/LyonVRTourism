using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameObject loadingTxt;
    public GameObject errorTxt;
    public GameObject errorBtn;

    private IEnumerator Start()
    {
        errorTxt.SetActive(false);
        loadingTxt.SetActive(true);
        errorBtn.SetActive(false);
        while (! (GeoManager.Instance.IsLoaded() || GeoManager.Instance.HasFailed()) || !(Controller.Instance.IsLoaded || Controller.Instance.HasFailed))
        {
            yield return null;
        }
        if (GeoManager.Instance.HasFailed() || Controller.Instance.HasFailed)
        {
            Debug.Log("Loading failed.");
            errorTxt.SetActive(true);
            loadingTxt.SetActive(false);
            errorBtn.SetActive(true);
        }
        else
        {
            // TODO 
            // if lien avec compte en ligne trouvé
            //     SceneManager.LoadScene("MapScene");
            // else
            Controller.Instance.SelectMenuLogout();
        }
    }

    public void TryAgain()
    {
        StartCoroutine(GeoManager.Instance.Init());
        StartCoroutine(Controller.Instance.InitQuests());
        StartCoroutine(Start());
    }
}
