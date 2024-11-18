using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public GameObject bombPrefab;    // Reference to the bomb prefab or UI element
        public string questionText;      // The question text
        public List<string> answerOptions; // List of answer options (2 wrong, 1 correct)
        public int correctAnswerIndex;    // Index of the correct answer in the options list
        public int duration;             // Duration for this question
    }

    [SerializeField] private List<Question> questions;  // List of questions/bombs
    [SerializeField] private TMP_Text m_BombTimer;
    [SerializeField] private TMP_Text m_QuestionText;
    [SerializeField] private Button[] answerButtons;    // Array of 3 buttons for answers

    private int timeRemaining;
    private bool isCountingDown = false;
    private int currentQuestionIndex = -1; // Track the current question

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
        m_QuestionText.text = currentQuestion.questionText;

        // Set up answer buttons
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < currentQuestion.answerOptions.Count)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].GetComponentInChildren<TMP_Text>().text = currentQuestion.answerOptions[i];
                int buttonIndex = i; // Capture index for the listener
                answerButtons[i].onClick.RemoveAllListeners(); // Remove previous listeners
                answerButtons[i].onClick.AddListener(() => SubmitAnswer(buttonIndex));
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false); // Hide unused buttons
            }
        }

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

    private void SubmitAnswer(int chosenIndex)
    {
        Question currentQuestion = questions[currentQuestionIndex];

        if (chosenIndex == currentQuestion.correctAnswerIndex)
        {
            DefuseBomb();
        }
        else
        {
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
        CancelInvoke("_tick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void WinGame()
    {
        Debug.Log("All bombs defused! You win!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}