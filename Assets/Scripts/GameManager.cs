using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List <GameObject> g_Question;
    [SerializeField] private TMP_Text m_BombTimer;
    private int duration = 8;
    private int timeRemaining;
    private bool isCountingDown = false;

    private void StartTimer()
    {
        if (!isCountingDown)
        {
            isCountingDown = true;
            timeRemaining = duration;
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
            isCountingDown = false;
        }
    }

    private void UpdateUI()
    {
        m_BombTimer.text = "Explodes in: " + timeRemaining.ToString();
    }
}
