using System.Collections;
using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public delegate void OnPauseEnd();
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Pause(float length, OnPauseEnd callback = null)
    {
        StartCoroutine(PauseCoroutine(length, callback));
    }
    private IEnumerator PauseCoroutine(float length, OnPauseEnd callback)
    {
        rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(length);
        rb.bodyType = RigidbodyType2D.Dynamic;
        callback?.Invoke();
    }
}
