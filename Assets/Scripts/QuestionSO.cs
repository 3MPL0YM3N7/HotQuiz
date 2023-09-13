using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string questionText = "Enter question text here";

    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;

    public string getQuestionText()
    {
        return questionText;
    }

    public int getCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

    public string getAnswer(int answerIndex)
    {
        return answers[answerIndex];
    }
}
