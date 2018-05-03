using UnityEngine;
using UnityEngine.UI;

public class UI_AnswerElement : MonoBehaviour
{
    private string answer;
    public Color successColor = Color.green;
    public Color failColor = Color.red;
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
            GetComponent<Image>().color = successColor;
        } else
        {
            GetComponent<Image>().color = failColor;
        }
        Button temp = Instantiate(touchToNext, buttonParent);
        temp.GetComponentInChildren<Text>().text = "Réponse : \r\n" + PlayQuestController.Instance.Answer;
    }
}

