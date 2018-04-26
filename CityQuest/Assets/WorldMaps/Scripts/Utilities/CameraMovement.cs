using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	public float heightMovementFactor = 2.5f;
	public float mouseSensitivy = 1.5f;
    public Plane mapPlane;

    public void Start()
    {
        Vector3 testView = new Vector3(0f, 7f, 0f);
        this.transform.SetPositionAndRotation(testView, Quaternion.Euler(new Vector3 (90, 0, 0)));
    }
}
