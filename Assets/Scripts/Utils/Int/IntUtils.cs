using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntUtils
{
    public static int Distance(this int a, int b)
    {
        return Mathf.Abs(a - b);
    }
}