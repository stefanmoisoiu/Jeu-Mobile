using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameScoreText : MonoBehaviour
{
    private int currentScore;
    [SerializeField] private TMP_Text previousScoreText, currentScoreText;

    public Action onScoreTextChange;
    [SerializeField] private UnityEvent uOnScoreTextChange;
    private void Start()
    {
        ScoreManager.onScoreChange += UpdateText;
    }
    private void UpdateText(int newScoreValue)
    {
        currentScore = newScoreValue;
        onScoreTextChange?.Invoke();
        uOnScoreTextChange?.Invoke();
    }
    private void UpdatePreviousText()
    {
        previousScoreText.text = currentScore.ToString();
    }
    private void UpdateCurrentText()
    {
        currentScoreText.text = currentScore.ToString();
    }
}
