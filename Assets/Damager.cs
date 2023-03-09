using UnityEngine;
using System;
using UnityEngine.Events;

public class Damager : MonoBehaviour, IDamager
{
    public Action onDamage;
    [SerializeField] private UnityEvent uOnDamage;
    public void DealtDamage()
    {
        onDamage?.Invoke();
        uOnDamage?.Invoke();
    }
}
