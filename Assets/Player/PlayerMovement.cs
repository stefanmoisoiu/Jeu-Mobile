using UnityEngine.Events;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private float groundCheckRadius;
    private bool isGrounded;

    [SerializeField] private float jumpForce, fallForce, attackJumpForce, jumpLength, fallCurveEndLength, attackJumpLength, maxJumpLength;
    private float currentJumpLength, currentFallLength, currentAttackJumpLength;
    private bool jumping, falling, attackJumping, usedMaxJump;

    [SerializeField] private AnimationCurve jumpCurve, fallCurve, attackJumpCurve;

    [SerializeField] private UnityEvent uOnJump, uOnFall, uOnLand, uOnAttackJump, uOnMaxJump;
    private static Action onJump, onFall, onLand, onAttackJump, onMaxJump;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        CheckGrounded();
    }
    private void FixedUpdate()
    {
        CheckJump();
    }
    private void CheckGrounded() => isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, groundLayer);
    private void CheckJump()
    {
        if (isGrounded && falling) Land();
        if (jumping && Input.touchCount == 0) StartFall();
        if (falling) Fall();
        if (Input.touchCount > 0 && isGrounded && !jumping && !attackJumping && !falling) StartJump();
        if (Input.touchCount > 0 && jumping) Jump();
        if (attackJumping) AttackJump();
    }
    private void StartJump()
    {
        currentJumpLength = 0;
        currentFallLength = 0;
        usedMaxJump = false;

        falling = false;
        jumping = true;
        attackJumping = false;

        uOnJump?.Invoke();
        onJump?.Invoke();
    }
    private void StartFall()
    {
        falling = true;
        jumping = false;
        attackJumping = false;

        uOnFall?.Invoke();
        onFall?.Invoke();
    }
    public void StartAttackJump()
    {
        currentAttackJumpLength = 0;

        falling = false;
        jumping = false;
        attackJumping = true;

        uOnAttackJump?.Invoke();
        onAttackJump?.Invoke();
    }
    private void Land()
    {
        falling = false;
        jumping = false;
        attackJumping = false;

        uOnLand?.Invoke();
        onLand?.Invoke();
    }
    private void Jump()
    {
        float jumpCurveAdvancement = currentJumpLength / jumpLength;
        float jumpCurveValue = jumpCurve.Evaluate(jumpCurveAdvancement);

        rb.velocity = new Vector2(rb.velocity.x, jumpCurveValue * jumpForce);

        currentJumpLength += Time.deltaTime;

        if (!usedMaxJump && currentJumpLength >= maxJumpLength)
        {
            usedMaxJump = true;
            uOnMaxJump?.Invoke();
            onMaxJump?.Invoke();
        }

        if (currentJumpLength >= jumpLength) StartFall();
    }
    private void AttackJump()
    {
        float attackJumpCurveAdvancement = currentAttackJumpLength / attackJumpLength;
        float attackJumpCurveValue = attackJumpCurve.Evaluate(attackJumpCurveAdvancement);

        rb.velocity = new Vector2(rb.velocity.x, attackJumpCurveValue * attackJumpForce);

        currentAttackJumpLength += Time.deltaTime;

        if (currentAttackJumpLength >= attackJumpLength) StartFall();
    }
    private void Fall()
    {
        float fallCurveAdvancement = Mathf.Min(currentFallLength / fallCurveEndLength, 1);
        float fallCurveValue = fallCurve.Evaluate(fallCurveAdvancement);

        rb.velocity = new Vector2(rb.velocity.x, fallCurveValue * fallForce);

        currentFallLength += Time.deltaTime;
        currentJumpLength = jumpLength;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
    }
}
