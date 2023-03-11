using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonEnemy : MonoBehaviour, IMovingEnemy
{
    [SerializeField] private float speed = 1f, curveHeightMult;
    [SerializeField] private AnimationCurve positionOffsetCurve;
    private float startXPos, startYPos, timeAdvancement;
    public float SpeedMult { get; set; }
    private Vector2 lastPos;
    [SerializeField] private LineRenderer previewLineRenderer;
    [SerializeField] private int previewPrecision = 100;
    [SerializeField] private Transform sprite;
    private void Start()
    {
        SpeedMult = 1;
        startXPos = transform.localPosition.x;
        startYPos = transform.localPosition.y;
        lastPos = transform.localPosition;

        Vector2 lrStartPos = previewLineRenderer.GetPosition(0);
        Vector2 lrEndPos = previewLineRenderer.GetPosition(1);
        previewLineRenderer.positionCount = previewPrecision;
        for (int i = 0; i < previewPrecision; i++)
        {
            float xPos = Mathf.Lerp(lrStartPos.x, lrEndPos.x, (float)i / (previewPrecision - 1));
            float yOffset = positionOffsetCurve.Evaluate((xPos - startXPos) / Mathf.Abs(startXPos)) * curveHeightMult;
            previewLineRenderer.SetPosition(i, new Vector3(xPos, yOffset, 0));
        }
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        timeAdvancement += Time.deltaTime;
        float xOffset = timeAdvancement * SpeedMult * speed;
        float yOffset = positionOffsetCurve.Evaluate(xOffset / Mathf.Abs(startXPos)) * curveHeightMult;
        transform.localPosition = new Vector3(startXPos + xOffset, startYPos + yOffset, transform.localPosition.z);

        Vector2 dir = ((Vector2)transform.localPosition - lastPos).normalized;
        Debug.DrawRay(transform.position, dir, Color.red);
        Debug.DrawLine(lastPos, transform.localPosition, Color.green);
        sprite.right = dir;
        lastPos = (Vector2)transform.localPosition;
    }
}
