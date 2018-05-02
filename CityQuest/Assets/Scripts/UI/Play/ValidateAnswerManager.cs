using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidateAnswerManager : MonoBehaviour
{

    public InputField inputAnswer;
    public Button touchToNext;
    public Transform parent;

    public void ValidateAnswer()
    {
        bool correct = PlayQuestController.Instance.CheckAnswer(inputAnswer.text);
        if (correct)
        {
            GetComponent<Image>().color = Color.green;
        } else
        {
            GetComponent<Image>().color = Color.red;
        }
        Instantiate(touchToNext, parent);

    }
}
