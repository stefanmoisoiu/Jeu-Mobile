using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string gameSceneName;
    public void StartGame()
    {
        FindObjectOfType<TransitionManager>().PlayTransition(TransitionManager.TransitionType.In, gameSceneName);
    }
}
