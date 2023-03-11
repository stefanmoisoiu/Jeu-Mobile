using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemyMove : MonoBehaviour, IMovingEnemy
{
    [SerializeField] private float speed = 1f;
    public float SpeedMult { get; set; }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.position += transform.right * transform.parent.localScale.x * speed * SpeedMult * Time.deltaTime;
    }
}
