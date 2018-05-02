using UnityEngine;
using UnityEngine.UI;

public class ButtonTransparencyManager : MonoBehaviour {

    // Use this for initialization
    void Start () {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
