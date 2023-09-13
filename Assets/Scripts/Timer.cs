using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToAnswerQuestion = 30f;
    [SerializeField] float timeToReviewAnswer = 10f;
    float timerValue;
    public float fillFraction;

    bool isAnsweringQuestion = false;

    public bool loadNextQuestion = false;

    public void cancelTimer()
    {
        timerValue = 0;
    }

    void updateTimer()
    {
        // simple frames timer
        timerValue -= Time.deltaTime;

        // toggle timer state (30f | 10f)
        if (timerValue <= 0)
        {
            if (!isAnsweringQuestion)
            {
                timerValue = timeToAnswerQuestion;
                isAnsweringQuestion = true;
                loadNextQuestion = true;
            }
            else if (isAnsweringQuestion)
            {
                timerValue = timeToReviewAnswer;
                isAnsweringQuestion = false;
            }
        }

        // calculating the fill for timer image
        if (isAnsweringQuestion)
        {
            fillFraction = timerValue / timeToAnswerQuestion;
        }
        else if (!isAnsweringQuestion)
        {
            fillFraction = timerValue / timeToReviewAnswer;
        }

        Debug.Log(isAnsweringQuestion + "\t" + timerValue + "\t" + fillFraction);
    }

    void Update()
    {
        updateTimer();
    }
}
