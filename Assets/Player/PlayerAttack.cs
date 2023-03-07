using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    private PlayerManager playerManager;
    [SerializeField] private Transform attackRight, attackLeft;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private LayerMask hittableLayer;
    [SerializeField] private float attackPauseLength, attackCooldown;
    public static Action onAttack, onAttackLeft, onAttackRight, onAttackBoth, onAttackEnd;
    [SerializeField] private UnityEvent uOnAttack, uOnAttackLeft, uOnAttackRight, uOnAttackBoth, uOnAttackEnd;
    private bool wasTouching, canAttack = true;
    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }
    private void Update()
    {
        if (Input.touchCount > 0 != wasTouching) TryAttack();
        wasTouching = Input.touchCount > 0;
    }
    private Collider2D[] LeftHittables() => Physics2D.OverlapBoxAll(attackLeft.position, attackSize, 0, hittableLayer);
    private Collider2D[] RightHittables() => Physics2D.OverlapBoxAll(attackRight.position, attackSize, 0, hittableLayer);
    private void TryAttack()
    {
        if (!canAttack) return;

        Collider2D[] leftHittables = LeftHittables();
        Collider2D[] rightHittables = RightHittables();

        Debug.Log($"Left: {leftHittables.Length}, Right: {rightHittables.Length}");

        if (leftHittables.Length > 0 || rightHittables.Length > 0)
        {
            onAttack?.Invoke();
            uOnAttack?.Invoke();
        }

        if (leftHittables.Length > 0 && rightHittables.Length > 0)
        {
            onAttackBoth?.Invoke();
            uOnAttackBoth?.Invoke();

            List<Collider2D> hittables = new();
            hittables.AddRange(leftHittables);
            hittables.AddRange(rightHittables);

            Attack(hittables.ToArray());
        }
        else if (leftHittables.Length > 0)
        {
            onAttackLeft?.Invoke();
            uOnAttackLeft?.Invoke();
            Attack(leftHittables);
        }
        else if (rightHittables.Length > 0)
        {
            onAttackRight?.Invoke();
            uOnAttackRight?.Invoke();
            Attack(rightHittables);
        }
    }
    private void Attack(Collider2D[] hittables)
    {
        foreach (Collider2D hittable in hittables) if (hittable.TryGetComponent(out IHittable hittableScript)) hittableScript.Hit();
        transform.position = new Vector3(transform.position.x, hittables[0].transform.position.y, transform.position.z);

        canAttack = false;
        Invoke(nameof(ResetAttack), attackCooldown);

        playerManager.Pause(attackPauseLength, AttackEnd);
    }
    private void AttackEnd()
    {
        onAttackEnd?.Invoke();
        uOnAttackEnd?.Invoke();
    }
    private void ResetAttack() => canAttack = true;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackLeft.position, attackSize);
        Gizmos.DrawWireCube(attackRight.position, attackSize);
    }
}
