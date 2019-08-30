using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class LayerAttribute : System.Attribute {}

#if UNITY_EDITOR
public sealed class LayerDrawer : OdinAttributeDrawer<LayerAttribute, int>
{
    [Obsolete]
    protected override void DrawPropertyLayout(IPropertyValueEntry<int> entry, LayerAttribute attribute, GUIContent label)
    {
        if (label != null)
            entry.SmartValue = UnityEditor.EditorGUILayout.LayerField(label, entry.SmartValue);
        else
            entry.SmartValue = UnityEditor.EditorGUILayout.LayerField("", entry.SmartValue);
    }
}
#endif