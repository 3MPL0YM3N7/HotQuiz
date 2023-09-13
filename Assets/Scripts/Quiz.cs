using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    // the QuestionSO variable with all the functions to return question/answer text
    QuestionSO currentQuestion;
    // list of questions to be initialized to currentQuestion
    [SerializeField] List<QuestionSO> questions;
    // the UI element where the question text is displayed
    [SerializeField] TextMeshProUGUI questionField;

    [Header("Answers")]
    // array of all the (4) button fields as game objects
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;

    [Header("Button Sprites")]
    [SerializeField] Sprite defaultAnswerButtonSprite;
    [SerializeField] Sprite correctAnswerButtonSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreTextfield;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;
    public bool quizIsComplete = false;

    void setButtonState(bool buttonState)
    {
        for (int index = 0; index < answerButtons.Length; ++index)
        {
            Button button = answerButtons[index].GetComponent<Button>();
            button.interactable = buttonState;
        }
    }

    // executed OnClick (Unity Editor --> answerButtons --> Button --> OnClick)
    public void checkUserAnswer(int answerIndex)
    {
        correctAnswerIndex = currentQuestion.getCorrectAnswerIndex();
        Image correctAnswerButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();

        if (answerIndex == correctAnswerIndex)
        {
            questionField.text = "That is correct.";
            correctAnswerButtonImage.sprite = correctAnswerButtonSprite;
            scoreKeeper.incrementCorrectAnswers();
        }
        else if (answerIndex != correctAnswerIndex)
        {
            string correctAnswer = currentQuestion.getAnswer(correctAnswerIndex);

            questionField.text = "Wrong. The answer was:\n" + correctAnswer;
            correctAnswerButtonImage.sprite = correctAnswerButtonSprite;
        }

        setButtonState(false);
        timer.cancelTimer();
    }

    void displayQuiz()
    {
        questionField.text = currentQuestion.getQuestionText();

        TextMeshProUGUI buttonAnswerField;
        for (int index = 0; index < answerButtons.Length; ++index)
        {
            buttonAnswerField = answerButtons[index].GetComponentInChildren<TextMeshProUGUI>();
            buttonAnswerField.text = currentQuestion.getAnswer(index);
        }
    }

    void setDefaultButtonSprites()
    {
        for (int index = 0; index < answerButtons.Length; ++index)
        {
            Image answerButtonImage = answerButtons[index].GetComponent<Image>();
            answerButtonImage.sprite = defaultAnswerButtonSprite;
        }
    }

    void getRandomQuestion()
    {
        int randomNumber = Random.Range(0, questions.Count);

        if (questions.Count > 0)
        {
            currentQuestion = questions[randomNumber];

            // check whether questions[randomNumber] exists
            if (questions.Contains(currentQuestion))
            {
                questions.Remove(currentQuestion);
            }
        }
    }

    void getNextQuestion()
    {
        if (questions.Count >= 0)
        {
            setButtonState(true);
            setDefaultButtonSprites();
            getRandomQuestion();
            displayQuiz();
            scoreKeeper.incrementQuestionsSeen();
            ++(progressBar.value);
        }
    }

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.value = 0;
        progressBar.maxValue = questions.Count;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        scoreTextfield.text = "Score: " + scoreKeeper.calculateScore() + "%";

        // if time is up before user answered
        if (timer.fillFraction <= 0.01 && !(timer.loadNextQuestion))
        {
            correctAnswerIndex = currentQuestion.getCorrectAnswerIndex();
            Image correctAnswerButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();

            string correctAnswer = currentQuestion.getAnswer(correctAnswerIndex);

            questionField.text = "Your time is up. Right answer was:\n" + correctAnswer;
            correctAnswerButtonImage.sprite = correctAnswerButtonSprite;

            setButtonState(false);
        }

        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                quizIsComplete = true;
                return;
            }

            getNextQuestion();
            timer.loadNextQuestion = false;
        }
    }
}
