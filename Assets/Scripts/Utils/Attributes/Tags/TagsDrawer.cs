using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class TagAttribute : System.Attribute {}

#if UNITY_EDITOR
public sealed class TagDrawer : OdinAttributeDrawer<TagAttribute, string>
{
    [Obsolete]
    protected override void DrawPropertyLayout(IPropertyValueEntry<string> entry, TagAttribute attribute, GUIContent label)
    {
        if (label != null)
            entry.SmartValue = UnityEditor.EditorGUILayout.TagField(label, entry.SmartValue);
        else
            entry.SmartValue = UnityEditor.EditorGUILayout.TagField("", entry.SmartValue);
    }
}
#endif