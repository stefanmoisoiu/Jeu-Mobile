using UnityEngine;
using System;
using UnityEngine.Events;
public class Hittable : MonoBehaviour, IHittable
{
    public Action onHit;
    [SerializeField] private UnityEvent uOnHit;
    public void Hit()
    {
        onHit?.Invoke();
        uOnHit?.Invoke();
    }
}
