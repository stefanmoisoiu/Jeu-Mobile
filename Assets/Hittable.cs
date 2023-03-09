using UnityEngine;
using System;
using UnityEngine.Events;
public class Hittable : MonoBehaviour, IHittable
{
    [SerializeField] private int scoreValue = 50;
    public Action onHit;
    [SerializeField] private UnityEvent uOnHit;
    public void Hit()
    {
        onHit?.Invoke();
        uOnHit?.Invoke();
        ScoreManager.AddScore(scoreValue);
    }
}
