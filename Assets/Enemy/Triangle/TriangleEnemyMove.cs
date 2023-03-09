using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemyMove : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.position += transform.right * transform.parent.localScale.x * speed * Time.deltaTime;
    }
}
