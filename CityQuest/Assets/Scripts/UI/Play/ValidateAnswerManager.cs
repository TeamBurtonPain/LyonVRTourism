using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidateAnswerManager : MonoBehaviour
{

    public InputField inputAnswer;

    public void ValidateAnswer()
    {
        bool correct = PlayQuestController.Instance.CheckAnswer(inputAnswer.text);
        if (correct)
        {
            GetComponent<Image>().color = Color.green;
        }
    }
}
