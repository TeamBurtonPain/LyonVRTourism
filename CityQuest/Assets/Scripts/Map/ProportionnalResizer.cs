using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProportionnalResizer : MonoBehaviour {
    public float size = 1;

    protected float coef = 0.01f;

    private void Update()
    {
        transform.localScale = coef * size * Camera.main.orthographicSize * new Vector3(1, 1, 1);
    }
}
