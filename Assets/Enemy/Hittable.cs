using UnityEngine;
using System;
using UnityEngine.Events;
public class Hittable : MonoBehaviour, IHittable
{
    [SerializeField] private int scoreValue = 50;
    public Action onEndHit, onStartHit;
    [SerializeField] private UnityEvent uOnEndHit, uOnStartHit;
    public void StartHit()
    {
        onStartHit?.Invoke();
        uOnStartHit?.Invoke();
    }
    public void EndHit()
    {
        onEndHit?.Invoke();
        uOnEndHit?.Invoke();
        ScoreManager.AddScore(scoreValue);
    }
}
