using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthImage : MonoBehaviour
{
    public Action onDelete;
    [SerializeField] private UnityEvent uOnDelete;

    public void StartDelete()
    {
        onDelete?.Invoke();
        uOnDelete?.Invoke();
    }
}
