
using UnityEngine.EventSystems;
using UnityEngine;

public class MapEventHandler : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public float perspectiveZoomSpeed = 0.05f;        // The rate of change of the field of view in perspective mode.
    public Camera mainCamera;

    //Prevents click trigger while dragging
    private bool dragging = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) //left button dragging only
        {
            dragging = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragging) //in case it was a left dragging
        {
            dragging = false;
        }
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

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Vector3 currPos = mainCamera.transform.position;
            float y = currPos.y + deltaMagnitudeDiff * perspectiveZoomSpeed;
            y = Mathf.Clamp(y, 0.1f, 10f);
            Vector3 testView = new Vector3(0f, y, 0f); 
            mainCamera.transform.SetPositionAndRotation(testView, Quaternion.Euler(new Vector3(90, 0, 0)));
    }
        
    }
}