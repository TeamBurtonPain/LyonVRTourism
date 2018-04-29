using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLoc : MonoBehaviour
{

    public Material normalMat;
    public Material selectedMat;

    private MapQuestGenerator parent;

    private Quest quest;
    private MapLocalizer localizer;

    public void LinkQuest(Quest q, MapLocalizer m, MapQuestGenerator parent)
    {
        quest = q;
        localizer = m;
        this.parent = parent;
        localizer.Localise(this.transform, q.Geolocalisation.x, q.Geolocalisation.y);

        UpdateSelect();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Whatever you want it to do.
            Controller.Instance.SelectedQuest = quest;
            parent.UpdateSelect();
        }
    }

    public void UpdateSelect()
    {
        if (Controller.Instance.SelectedQuest == quest)
        {
            GetComponent<Renderer>().material = selectedMat;
        }
        else
        {
            GetComponent<Renderer>().material = normalMat;
        }
    }
}
