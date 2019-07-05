using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FloatRadius))]
public class RadiusEditor : Editor
{
    void OnSceneGUI()
    {
        FloatRadius script = (FloatRadius)target;
        Handles.color = script.radiusColor;
        Undo.RecordObject(script, "Change radius");
        script.radius = Handles.RadiusHandle(script.transform.rotation, script.transform.position, script.radius);
    }
}