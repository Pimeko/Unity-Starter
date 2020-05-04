using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameObjectTools : EditorWindow
{
    string tagValue = "";

    [MenuItem("Custom/GameObject/Change tag recursively")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(GameObjectTools));
    }
    
    void OnGUI()
    {
        GUILayout.Label("Apply tag recursively", EditorStyles.boldLabel);

        GUILayout.Label("Tag", EditorStyles.label);
        tagValue = EditorGUILayout.TextField("", tagValue, GUILayout.Width(300));

        if (GUILayout.Button("Apply", GUILayout.Width(150)))
        {
            if (tagValue == "")
                return;
            
            Selection.activeGameObject.ChangeTagRecursively(tagValue);
        }
    }

    [MenuItem("Custom/GameObject/ClearComponents")]
    public static void ClearComponents()
    {
        foreach (var comp in Selection.activeGameObject.GetComponents<Component>())
        {
            if (!(comp is Transform))
            {
                DestroyImmediate(comp);
            }
        }
    }
}