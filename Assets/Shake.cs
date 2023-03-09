using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public ShakeInfo[] shakes;
    public void ApplyShake(int shakeInfoIndex)
    {
        ShakeInfo shake = shakes[shakeInfoIndex];
        ApplyShake(shake.magnitude, shake.duration, shake.frequency, shake.direction);
    }
    public void ApplyShake(float magnitude, float duration, int frequency = 3, Vector2 direction = default)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude, frequency, direction));
    }
    private IEnumerator ShakeCoroutine(float duration, float magnitude, int frequency, Vector2 direction)
    {
        Vector2 previousOffset = Vector2.zero;
        Vector2 offset = Vector2.zero;
        Vector2 deltaOffset = Vector2.zero;
        Vector2 lastTargetOffsetPos = Vector2.zero;
        Vector2 targetOffsetPos = Vector2.zero;

        int frequencyAdvancement = -1;
        float lerpLength = duration / (float)frequency;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            int currentFrequencyAdvancement = Mathf.FloorToInt(elapsed / lerpLength);
            if (currentFrequencyAdvancement != frequencyAdvancement)
            {
                frequencyAdvancement = currentFrequencyAdvancement;

                lastTargetOffsetPos = targetOffsetPos;
                targetOffsetPos = frequencyAdvancement != frequency - 1 ? Random.insideUnitCircle * magnitude * (1 - elapsed / duration) : Vector2.zero;

                if (direction != default)
                    targetOffsetPos *= direction.normalized;
            }
            offset = Vector2.Lerp(lastTargetOffsetPos, targetOffsetPos, elapsed / duration * frequency - frequencyAdvancement);
            deltaOffset = offset - previousOffset;
            previousOffset = offset;
            transform.localPosition += new Vector3(deltaOffset.x, deltaOffset.y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
[System.Serializable]
public struct ShakeInfo
{
    public string name;
    public float magnitude;
    public float duration;
    public int frequency;
    public Vector2 direction;
    public ShakeInfo(string name = "Shake", float magnitude = 1, float duration = .5f, int frequency = 3, Vector2 direction = default)
    {
        this.name = name;
        this.magnitude = magnitude;
        this.duration = duration;
        this.frequency = frequency;
        this.direction = direction;
    }
}