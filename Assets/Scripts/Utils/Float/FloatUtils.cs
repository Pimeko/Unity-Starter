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
}