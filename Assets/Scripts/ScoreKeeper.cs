using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    // float --> float division rather than integer division
    float correctAnswers = 0f;
    float questionsSeen = 0f;

    public float getCorrectAnswers()
    {
        return correctAnswers;
    }

    public void incrementCorrectAnswers()
    {
        ++correctAnswers;
    }

    public float getQuestionsSeen()
    {
        return questionsSeen;
    }

    public void incrementQuestionsSeen()
    {
        ++questionsSeen;
    }

    public float calculateScore()
    {
        return correctAnswers / questionsSeen * 100;
    }
}
