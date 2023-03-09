using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
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
        currentHealth--;
        uiPlayerHealth.AddMissingHealthImages(currentHealth);
    }
}
