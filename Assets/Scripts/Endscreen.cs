using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Endscreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreTextfield;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void showFinalScore()
    {
        finalScoreTextfield.text = "Score: " + scoreKeeper.calculateScore() + "%";
    }
}
