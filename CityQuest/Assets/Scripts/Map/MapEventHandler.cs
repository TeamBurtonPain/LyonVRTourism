
using UnityEngine.EventSystems;
using UnityEngine;

public class MapEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    
    public float zoomSpeed = 1f;        // Amplitude of the zoom while pinching.
    
    protected bool isReadyDrag = false;
    protected bool isReadyMove = false;
    
    public int mapSize = 10;

    //Prevents click trigger while dragging

    private Vector2 initDrag;
    private Vector3 initPosition;
    private Vector2 oldTemp;

    private Touch startZoom0;
    private Touch startZoom1;
    private float initTouchDeltaMag;
    private float initSize;


    public void OnDrag(PointerEventData eventData)
    {
        
        {
            if (!isReadyMove)
            {
                // Move
                initDrag = GetPos();
                initPosition = Camera.main.transform.position;
                isReadyMove = true;
            }

            float coeff = Camera.main.orthographicSize / Camera.main.pixelWidth;

            Vector2 newDrag = GetPos();

            Vector2 dragVector = newDrag - initDrag;
            Vector3 newView = initPosition - new Vector3(dragVector.x, 0, dragVector.y) * coeff;

            newView.x = Mathf.Clamp(newView.x,-mapSize + Camera.main.orthographicSize * Camera.main.aspect, mapSize - Camera.main.orthographicSize * Camera.main.aspect);
            newView.z = Mathf.Clamp(newView.z, -mapSize + Camera.main.orthographicSize, mapSize - Camera.main.orthographicSize);

            Camera.main.transform.position = newView;
            oldTemp = newDrag;
        }

        // Zoom
        if (Input.touchCount == 2)
        {
            if (!isReadyDrag)
            {
                isReadyDrag = true;
                startZoom0 = Input.GetTouch(0);
                startZoom1 = Input.GetTouch(1);
                initTouchDeltaMag = (startZoom0.position - startZoom1.position).magnitude;
                initSize = Camera.main.orthographicSize;
            }

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = initTouchDeltaMag / touchDeltaMag;

            Camera.main.orthographicSize = deltaMagnitudeDiff * initSize;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 0.05f, 5f);
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Reset();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    private void Reset()
    {
        isReadyDrag = false;
        isReadyMove = false;
    }
    private Vector2 GetPos()
    {
        Vector2 v = new Vector2(0, 0);

        if (Input.touchCount == 0)
        {
            v = Input.mousePosition;
        }
        else
        {

            for (int i = 0; i < Input.touchCount; i++)
            {
                v += Input.GetTouch(i).position;
            }
            v /= Input.touchCount;

        }

        return v;
    }

}
