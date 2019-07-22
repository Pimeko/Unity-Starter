using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUtils
{
    public static void DrawPoint(Vector3 position)
    {
        Debug.DrawLine(position - new Vector3(1, 0, 0), position + new Vector3(1, 0, 0), Color.white);
        Debug.DrawLine(position - new Vector3(0, 0, 1), position + new Vector3(0, 0, 1), Color.white);
        Debug.DrawLine(position - new Vector3(0, 1, 0), position + new Vector3(0, 1, 0), Color.white);
    }
}