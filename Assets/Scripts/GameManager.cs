using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public GameObject bombPrefab;    // Reference to the bomb prefab or UI element
        public string questionText;      // The question text
        public string correctAnswer;     // The correct answer
        public int duration;             // Duration for this question
        public bool allowSecondAttempt;  // If true, player can have a second attempt
    }

    [SerializeField] private List<Question> questions;  // List of questions/bombs
    [SerializeField] private TMP_Text m_BombTimer;
    [SerializeField] private TMP_Text m_QuestionText;
    [SerializeField] private TMP_InputField m_AnswerInput;

    private int timeRemaining;
    private bool isCountingDown = false;
    private int currentQuestionIndex = -1; // Track the current question
    private bool usedSecondAttempt = false; // Track if the player has used their second attempt

    private void Start()
    {
        StartNextQuestion();
    }

    private void StartNextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex >= questions.Count)
        {
            WinGame();  // Player wins if all questions are defused
            return;
        }

        Question currentQuestion = questions[currentQuestionIndex];
        timeRemaining = currentQuestion.duration;
        usedSecondAttempt = false;  // Reset second attempt status
        m_QuestionText.text = currentQuestion.questionText;
        m_AnswerInput.text = "";
        m_AnswerInput.ActivateInputField();

        UpdateUI();
        StartTimer();
    }

    private void StartTimer()
    {
        if (!isCountingDown)
        {
            isCountingDown = true;
            Invoke("_tick", 1f);
        }
    }

    private void _tick()
    {
        timeRemaining--;
        if (timeRemaining > 0)
        {
            Invoke("_tick", 1f);
            UpdateUI();
        }
        else
        {
            UpdateUI();
            LoseGame();
            isCountingDown = false;
        }
    }

    private void UpdateUI()
    {
        m_BombTimer.text = "Explodes in: " + timeRemaining.ToString();
    }

    public void SubmitAnswer()
    {
        Question currentQuestion = questions[currentQuestionIndex];
        bool isCorrect = m_AnswerInput.text.Equals(currentQuestion.correctAnswer, System.StringComparison.OrdinalIgnoreCase);

        if (isCorrect)
        {
            DefuseBomb();
        }
        else if (currentQuestion.allowSecondAttempt && !usedSecondAttempt)
        {
            Debug.Log("Incorrect answer. You have one more attempt.");
            usedSecondAttempt = true;
            m_AnswerInput.text = ""; // Clear input for the second attempt
            m_AnswerInput.ActivateInputField();
        }
        else
        {
            Debug.Log("Incorrect answer. No more attempts allowed.");
            LoseGame();
        }
    }

    private void DefuseBomb()
    {
        Debug.Log("Bomb defused!");
        isCountingDown = false;
        CancelInvoke("_tick");
        StartNextQuestion();
    }

    private void LoseGame()
    {
        Debug.Log("Boom! You lost!");
        isCountingDown = false;
    }

    private void WinGame()
    {
        Debug.Log("All bombs defused! You win!");
    }
}