using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUtils
{
    public static void DrawPoint(Vector3 position, Color color, float duration)
    {
        Debug.DrawLine(position - new Vector3(1, 0, 0), position + new Vector3(1, 0, 0), color, duration);
        Debug.DrawLine(position - new Vector3(0, 0, 1), position + new Vector3(0, 0, 1), color, duration);
        Debug.DrawLine(position - new Vector3(0, 1, 0), position + new Vector3(0, 1, 0), color, duration);
    }

    public static void PrintNull(object o)
    {
        Debug.Log(o == null ? "null" : "not null");
    }
}