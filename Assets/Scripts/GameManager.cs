using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    Endscreen endscreen;

    // Endscreen --> Replay Button --> onClick
    public void onReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endscreen = FindObjectOfType<Endscreen>();
    }

    void Start()
    {
        quiz.gameObject.SetActive(true);
        endscreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (quiz.quizIsComplete)
        {
            quiz.gameObject.SetActive(false);
            endscreen.gameObject.SetActive(true);
            endscreen.showFinalScore();
        }
    }
}
