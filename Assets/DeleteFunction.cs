using UnityEngine;
using UnityEngine.Events;
using System;
public class DeleteFunction : MonoBehaviour
{
    public Action onDelete;
    [SerializeField] private UnityEvent uOnDelete;
    public void Delete()
    {
        onDelete?.Invoke();
        uOnDelete?.Invoke();
        Destroy(gameObject);
    }
}
