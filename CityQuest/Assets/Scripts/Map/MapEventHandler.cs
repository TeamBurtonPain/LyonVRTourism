
using UnityEngine.EventSystems;
using UnityEngine;

public class MapEventHandler : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public float zoomSpeed = 0.01f;        // Amplitude of the zoom while pinching.
    public int mapSize = 10;

    //Prevents click trigger while dragging
    private float prevDragX;
    private float prevDragY;
    private bool zooming = false;


    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!zooming)
        {
            float coeff = Camera.main.orthographicSize / Camera.main.pixelWidth;
            Vector3 dragVector = new Vector3(Input.mousePosition.x - prevDragX, 0, Input.mousePosition.y - prevDragY);
            Vector3 newView = Camera.main.transform.position - dragVector * coeff;
            prevDragX = Input.mousePosition.x;
            prevDragY = Input.mousePosition.y;
            Camera.main.transform.SetPositionAndRotation(newView, Camera.main.transform.rotation);
        }
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

            Camera.main.orthographicSize += deltaMagnitudeDiff * zoomSpeed;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 0.1f, 5f);
        }
        Camera.main.transform.SetPositionAndRotation(new Vector3(
               Mathf.Clamp(Camera.main.transform.position.x, -mapSize / 2 + Camera.main.orthographicSize / 2, mapSize / 2 - Camera.main.orthographicSize / 2),
               Camera.main.transform.position.y,
               Mathf.Clamp(Camera.main.transform.position.z, -mapSize / 2 + Camera.main.orthographicSize / 2, mapSize / 2 - Camera.main.orthographicSize / 2)
               ), Camera.main.transform.rotation);

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.touchCount == 2)
            zooming = true;
        if (Input.touchCount == 1)
            zooming = false;
        prevDragX = Input.mousePosition.x;
        prevDragY = Input.mousePosition.y;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}