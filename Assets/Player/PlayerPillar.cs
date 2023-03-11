using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;

public class PlayerPillar : MonoBehaviour
{
    [SerializeField] private float invincibleTime = 1f;
    private bool invincible = false;
    public Action onPillarDamage;
    [SerializeField] private UnityEvent uOnPillarDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamager damager)) return;
        damager.DealtDamage();
        if (invincible) return;

        onPillarDamage?.Invoke();
        uOnPillarDamage?.Invoke();

        invincible = true;
        Invoke(nameof(ResetInvincible), invincibleTime);
    }
    private void ResetInvincible() => invincible = false;
}
