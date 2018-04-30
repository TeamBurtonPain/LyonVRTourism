using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLoc : MonoBehaviour
{

    public Material normalMat;
    public Material selectedMat;
    public Material tooFarMat;

    public Canvas centerButton;

    private MapQuestGenerator parent;

    private Quest quest;
    private MapLocalizer localizer;

    private void Start()
    {
        InvokeRepeating("CheckDistance", 0.1f, 1);
        centerButton.gameObject.SetActive(false);
    }

    public void LinkQuest(Quest q, MapLocalizer m, MapQuestGenerator parent)
    {
        quest = q;
        localizer = m;
        this.parent = parent;
        localizer.Localise(this.transform, q.Geolocalisation.x, q.Geolocalisation.y);
        
        CheckDistance();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Controller.Instance.SelectedQuest = quest;
            parent.UpdateSelect();
        }
    }

    public void CheckDistance()
    {
        if (Controller.Instance.SelectedQuest == quest)
        {
            GetComponent<Renderer>().material = selectedMat;
            centerButton.gameObject.SetActive(true);
        }
        else
        {
            centerButton.gameObject.SetActive(false);
            if (GeoManager.Instance.IsUserNear(quest.Geolocalisation))
            {
                GetComponent<Renderer>().material = normalMat;
            }
            else
            {
                GetComponent<Renderer>().material = tooFarMat;
            }
        }
    }

    public void CenterCam()
    {
        Camera.main.transform.position = new Vector3(this.transform.position.x, Camera.main.transform.position.y, this.transform.position.z);
    }
}
