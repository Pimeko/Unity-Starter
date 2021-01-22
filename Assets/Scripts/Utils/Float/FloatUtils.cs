using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatUtils
{
    public static float Distance(this float a, float b)
    {
        return Mathf.Abs(a - b);
    }

    public static void ClampUp(ref this float value, float max)
    {
        if (value > max)
            value = max;
    }

    public static void ClampDown(ref this float value, float min)
    {
        if (value < min)
            value = min;
    }

    public static void Clamp(ref this float value, float min, float max)
    {
        value = Mathf.Clamp(value, min, max);
    }

    public static float ChangeRange(ref this float value, Vector2 oldRange, Vector2 newRange)
    {
        return (((value - oldRange.x) * (newRange.y - newRange.x)) / (oldRange.y - oldRange.x)) + newRange.x;
    }
}