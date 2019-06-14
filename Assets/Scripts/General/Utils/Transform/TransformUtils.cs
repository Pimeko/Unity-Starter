using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformUtils
{
    public static IEnumerator MoveTo(float from, float to, float duration,
        Action<float> onChange = null, Action onOver = null)
    {
        float t = 0;
        float currentPosition = from;
        
        while (t < duration)
        {
            currentPosition = Mathf.Lerp(from, to, (t / duration));
            if (onChange != null)
                onChange(currentPosition);
            t += Time.deltaTime;
            yield return null;
        }
        if (onChange != null)
            onChange(to);

        if (onOver != null)
            onOver();
    }

    public static IEnumerator MoveTo(Vector3 from, Vector3 to, float duration,
        Action<Vector3> onChange = null, Action onOver = null)
    {
        float t = 0;
        Vector3 currentPosition = from;
        
        while (t < duration)
        {
            currentPosition = Vector3.Lerp(from, to, (t / duration));
            if (onChange != null)
                onChange(currentPosition);
            t += Time.deltaTime;
            yield return null;
        }
        if (onChange != null)
            onChange(to);

        if (onOver != null)
            onOver();
    }

    public static float Distance(float a, float b)
    {
        return Mathf.Abs(a - b);
    }
}