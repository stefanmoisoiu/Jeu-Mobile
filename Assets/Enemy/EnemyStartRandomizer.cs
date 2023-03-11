using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStartRandomizer : MonoBehaviour
{
    [SerializeField] private bool scaleX, rotate;
    [SerializeField] private float rotateValue = 30;
    private void Start()
    {
        if (scaleX) transform.localScale = new Vector3(transform.localScale.x * Random.Range(0, 2) == 0 ? 1 : -1, transform.localScale.y, transform.localScale.z);
        if (rotate) transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 2) == 0 ? rotateValue : -rotateValue);
    }
}
