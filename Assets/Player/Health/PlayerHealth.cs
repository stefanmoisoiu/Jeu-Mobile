using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private UnityEvent uOnTakeDamage, uOnDie;
    public static Action onTakeDamage, onDie;
    private UIPlayerHealth uiPlayerHealth;
    [SerializeField] private int startHealth = 3;
    private int currentHealth;
    private void Start()
    {
        currentHealth = startHealth;
        uiPlayerHealth = FindObjectOfType<UIPlayerHealth>();
        uiPlayerHealth.AddMissingHealthImages(currentHealth);
    }
    public void TakeDamage()
    {
        if (currentHealth <= 0) return;

        currentHealth--;
        uiPlayerHealth.AddMissingHealthImages(currentHealth);

        uOnTakeDamage?.Invoke();
        onTakeDamage?.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        uOnDie?.Invoke();
        onDie?.Invoke();

        FindObjectOfType<HighScoreSave>().TrySaveHighScore(ScoreManager.score);
    }
}
