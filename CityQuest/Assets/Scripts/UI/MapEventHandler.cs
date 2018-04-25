
using UnityEngine.EventSystems;
using UnityEngine;

public class MapEventHandler : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public float perspectiveZoomSpeed = 0.01f;        // The rate of change of the field of view in perspective mode.

    //Prevents click trigger while dragging
    private float prevDragX;
    private float prevDragY;

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        float coeff = Camera.main.orthographicSize / Camera.main.pixelWidth;
        Vector3 dragVector = new Vector3(Input.mousePosition.x - prevDragX, 0, Input.mousePosition.y - prevDragY);
        Vector3 newView = Camera.main.transform.position - dragVector * coeff;
        prevDragX = Input.mousePosition.x;
        prevDragY = Input.mousePosition.y;
        Camera.main.transform.SetPositionAndRotation(newView, Camera.main.transform.rotation);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        prevDragX = Input.mousePosition.x;
        prevDragY = Input.mousePosition.y;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    void Update()
    {
        
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float touchCenterX = touchOne.position.x;
            float touchCenterY = touchOne.position.y;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Camera.main.orthographicSize += deltaMagnitudeDiff * perspectiveZoomSpeed;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 0.1f, 5f);
        }
        
    }
}