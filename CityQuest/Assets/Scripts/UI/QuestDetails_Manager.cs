using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestDetails_Manager : MonoBehaviour
{

    public Button scroll;
    public GameObject questDetails;
    public Image star1;
    public Image star2;
    public Image star3;
    public Image star4;
    public Image star5;
    public Text title;
    public Text description;
    public List<Image> stars;
    private Color starRated;
    private Color starUnrated;
    private Quest quest;

    private void Start()
    {
        starRated = Color.yellow;
        starUnrated = Color.gray;
        stars = new List<Image>
        {
            star1,
            star2,
            star3,
            star4,
            star5
        };
        UpdateContent();
    }

    public void UpdateContent()
    {
        //Mise à jour de la fenêtre cachée avec les informations correspondantes à l'éléments cliqué
        if (Controller.Instance.SelectedQuest != null)
        {
            Quest actualQuest = Controller.Instance.SelectedQuest;
            StarsManager(actualQuest.Statistics);
            description.text = actualQuest.Description;
            title.text = actualQuest.Title;
        }
        else
        {
            title.text = "No Quest selected";
        }
    }

    /// <summary>
    /// If a quest is selected on the map, the details of this quest are updated on the hidden interface
    /// Adds to the scroll button an Animator, which handles the hiding and showing of the details interface
    /// The trigger is simply a click on the scroll button
    /// </summary>
    public void ScrollButtonListener()
    {
        if (Controller.Instance.SelectedQuest != null)
        {
            UpdateContent();
            //Affichage de la fenêtre cachée
            Animator animationObj = questDetails.GetComponentInChildren<Animator>();
            animationObj.SetTrigger("clic");
        }
    }

    /// <summary>
    /// Handles the transription between the numeric rank of a quest, and its graphic representation with color filled stars
    /// </summary>
    /// <param name="statistics">The statistic object of the selected quest which contains its mark, given by the users</param>
    public void StarsManager(QuestStatistics statistics)
    {
        double mark = statistics.ComputeMean();

        //TODO gerer le cas sans moyenne
        if (double.IsNaN(mark))
            mark = 0;

        for (int i = 0; i < mark && i < stars.Count; i++)
        {
            stars[i].color = starRated;
        }
        for (int i = (int)mark; i < stars.Count; i++)
        {
            stars[i].color = starUnrated;
        }
    }

    public void StartButtonListener()
    {
        Controller.Instance.StartQuest();
    }
}
