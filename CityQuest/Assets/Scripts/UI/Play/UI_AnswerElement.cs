using UnityEngine;
using UnityEngine.UI;

public class UI_AnswerElement : MonoBehaviour
{
    private string answer;
    public Text answerText;
    private Button touchToNext;
    private Transform buttonParent;

    public void LinkAnswer(string ans, Button touchToNext, Transform buttonParent)
    {
        answer = ans;
        answerText.text = answer;
        this.touchToNext = touchToNext;
        this.buttonParent = buttonParent;
    }

    public void SelectAnswer()
    {
        // TODO : Lancer la scene suivante
        if(PlayQuestController.Instance.CheckAnswer(answer))
        {
            GetComponent<Image>().color = Color.green;
        } else
        {
            GetComponent<Image>().color = Color.red;
        }
        Instantiate(touchToNext, buttonParent);
    }
}

