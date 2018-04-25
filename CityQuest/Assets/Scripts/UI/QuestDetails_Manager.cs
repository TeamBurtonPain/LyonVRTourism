using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestDetails_Manager : MonoBehaviour {

    public Button scroll;
    public GameObject questDetails;
    public Image star1;
    public Image star2;
    public Image star3;
    public Image star4;
    public Image star5;
    public Text description;
    public List<Image> stars;
    private Color starRated;
    private Color starUnrated;
    private Quest quest;

    private void Awake()
    {
        /*
        StatsQuestUnit test1 = new StatsQuestUnit(new User("Jacky")); test1.Mark = 3;
        StatsQuestUnit test2 = new StatsQuestUnit(new User("Michel")); test1.Mark = 5;
        StatsQuest testStats = new StatsQuest(test1, test2);
        */
        starRated = Color.yellow;
        starUnrated = Color.gray;
        stars.Add(star1);
        stars.Add(star2);
        stars.Add(star3);
        stars.Add(star4);
        stars.Add(star5);
    }

    public void ScrollButtonListener()
    {
        //Mise à jour de la fenêtre cachée avec les informations correspondantes à l'éléments cliqué
        //Quest quest = new Quest()...
        StarsManager(/* quest.Statistics */);
        DescriptionManager(/*quest.Description*/"La Doua est un campus situé sur un ancien camp militaire dans la commune de Villeurbanne, au nord-est de l'agglomération lyonnaise. Il est bordé par le parc de la Tête d'Or et le tennis-club de Lyon à l'ouest, par le Rhône et le parc de la Feyssine au nord/nord-est, et enfin par Villeurbanne et le 6e arrondissement de Lyon au sud. Il constitue le plus grand site universitaire de l'agglomération lyonnaise avec une superficie de 100 hectares1.");

        //Affichage de la fenêtre cachée
        Animator animationObj = questDetails.GetComponentInChildren<Animator>();
        animationObj.SetTrigger("clic");
    }

    public void DescriptionManager(string text)
    {
        description.text = text;
    }

    public void StarsManager(/*StatsQuest statistics*/)
    {
        double mark = 0;
        /*
        foreach(StatsQuestUnit element in sta   tistics.Marks)
        {
            mark += element.Mark;
        }
        mark = System.Math.Truncate(mark / statistics.Marks.Count);
        */
        // A ENLEVER
        mark = 4;
        //-----------
        for(int i = 0; i < mark; i++)
        {
            stars[i].color = starRated;
        }
        for(int i = (int)mark; i < stars.Count; i++)
        {
            stars[i].color = starUnrated;
        }
    }
}
