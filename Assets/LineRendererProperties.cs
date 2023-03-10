using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererProperties : MonoBehaviour
{
    [SerializeField] private bool useWorldSpace = true;
    [SerializeField] private float textureOffsetSpeed = 0;
    [SerializeField] private bool startScaleEffect = true;
    [SerializeField] private float startScaleEffectDuration = 1;
    public delegate void OnScaleEffectEnd();
    private LineRenderer lr;
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = useWorldSpace;
        if (startScaleEffect) StartScaleEffect(true);
    }
    private void Update()
    {
        lr.material.mainTextureOffset = new Vector2(Time.time * -textureOffsetSpeed, 0);
    }
    private void OnValidate()
    {
        if (lr == null) lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = useWorldSpace;
    }

    public void StartScaleEffect(bool show) => StartCoroutine(StartScaleEffectIE(show));
    public void StartScaleEffect(bool show, OnScaleEffectEnd callback) => StartCoroutine(StartScaleEffectIE(show, callback));
    private IEnumerator StartScaleEffectIE(bool show, OnScaleEffectEnd callback = null)
    {
        float time = 0;
        float lrStartWidth = lr.startWidth;
        while (time < startScaleEffectDuration)
        {
            time += Time.deltaTime;

            float value0 = show ? 0 : lrStartWidth;
            float value1 = show ? lrStartWidth : 0;

            lr.startWidth = Mathf.Lerp(value0, value1, Mathf.Sin(time / startScaleEffectDuration * Mathf.PI / 2));
            lr.endWidth = Mathf.Lerp(value0, value1, Mathf.Sin(time / startScaleEffectDuration * Mathf.PI / 2));
            yield return null;
        }
        callback?.Invoke();
    }
}
