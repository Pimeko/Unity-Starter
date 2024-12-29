using System;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif
using UnityEditor;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class LayerAttribute : Attribute {}

#if UNITY_EDITOR
public class LayerDrawer : OdinValueDrawer<int>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        // Dessiner le champ en tant que LayerField
        this.ValueEntry.SmartValue = label != null 
            ? EditorGUILayout.LayerField(label, this.ValueEntry.SmartValue)
            : EditorGUILayout.LayerField(this.ValueEntry.SmartValue);
    }
}
#endif