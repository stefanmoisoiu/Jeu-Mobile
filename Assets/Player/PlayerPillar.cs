using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;

public class PlayerPillar : MonoBehaviour
{
    public Action onPillarDamage;
    [SerializeField] private UnityEvent uOnPillarDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamager damager)) return;
        onPillarDamage?.Invoke();
        uOnPillarDamage?.Invoke();
        damager.DealtDamage();
    }
}
