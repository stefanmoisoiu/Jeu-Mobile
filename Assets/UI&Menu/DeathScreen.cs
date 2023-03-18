using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    private TransitionManager transitionManager;
    [SerializeField] private string menuSceneName, gameSceneName;
    private void Start()
    {
        transitionManager = FindObjectOfType<TransitionManager>();
    }
    public void RestartGame()
    {
        transitionManager.PlayTransition(TransitionManager.TransitionType.In, gameSceneName);
    }
    public void GoToMenu()
    {
        transitionManager.PlayTransition(TransitionManager.TransitionType.In, menuSceneName);
    }
}
