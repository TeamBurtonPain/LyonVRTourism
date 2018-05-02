using UnityEngine;
using UnityEngine.UI;

public class UI_AnswerElement : MonoBehaviour
{
    private string answer;
    public Text answerText;

    public void LinkAnswer(string ans)
    {
        answer = ans;
        answerText.text = answer;
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
    }
}

