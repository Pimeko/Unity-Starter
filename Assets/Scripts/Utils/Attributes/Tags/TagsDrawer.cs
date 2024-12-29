using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class TagAttribute : System.Attribute { }

#if UNITY_EDITOR
public sealed class TagDrawer : OdinAttributeDrawer<TagAttribute, string>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        // Dessiner le champ en tant que TagField
        this.ValueEntry.SmartValue = label != null
            ? EditorGUILayout.TagField(label, this.ValueEntry.SmartValue)
            : EditorGUILayout.TagField(this.ValueEntry.SmartValue);
    }
}
#endif