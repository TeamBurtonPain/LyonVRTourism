using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    public RawImage background;
    public RawImage preview;
    public GameObject bgCam;
    void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No camera detected");
            camAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name,Screen.width,Screen.height);
            }
        }

        if (backCam == null)
        {
            Debug.Log("Unable to find back camera");
            return;
        }

        backCam.Play();
        background.texture = backCam;
        bgCam.SetActive(false);

        camAvailable = true;
    }

    void Update()
    {
        if (!camAvailable)
            return;

        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0,0,orient);
    }

    public void CameraSnapshot()
    {
        StartCoroutine(TakePhoto());
    }
    
    IEnumerator TakePhoto()
    {
        // NOTE - you almost certainly have to do this here:

        yield return new WaitForEndOfFrame();

        // it's a rare case where the Unity doco is pretty clear,
        // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
        // be sure to scroll down to the SECOND long example on that doco page 

        Texture2D photo = new Texture2D(backCam.width, backCam.height);
        photo.SetPixels(backCam.GetPixels());
        photo.Apply();

        preview.texture = photo;   

        //Encode to a PNG
        string screenShotName = "TB" + System.DateTime.Now.ToString("dd-MM-yyyy-HHmmss")+".png";
        byte[] bytes = photo.EncodeToPNG();
        //Write out the PNG. Of course you have to substitute your_path for something sensible

        //File.WriteAllBytes(Application.persistentDataPath+screenShotName, bytes);
        bgCam.SetActive(false);
        Debug.Log("Photo prise.");
    }

    public void CancelSnapshot()
    {
        bgCam.SetActive(false);
    }

    public void LaunchSnapshot()
    {
        bgCam.SetActive(true);
    }

}
