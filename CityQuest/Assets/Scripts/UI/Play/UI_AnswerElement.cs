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
        Controller.Instance.CheckAnswer(answer);
    }
}

