using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class Questions : ScriptableObject
{
    [Header("Question Description")]
    [Tooltip("The question that is displayed for the player")]
    [SerializeField] private TMP_Text m_question;

    [Header("Game Setup")]
    [Tooltip("Setup for the game")]
    [SerializeField] private int m_Timer;
}
