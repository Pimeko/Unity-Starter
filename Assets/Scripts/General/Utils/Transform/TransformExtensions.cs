using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    private static bool IsNull<T>(T obj)
    {
        return EqualityComparer<T>.Default.Equals(obj,default(T));
    }

    public static T GetComponentOrInParent<T>(this Transform t)
    {
        if (IsNull(t))
            return default(T);
        T component = t.GetComponent<T>();
        if (!IsNull(component))
            return component;
        return GetComponentOrInParent<T>(t.parent);
    }
    
    public static Transform GetTopLevelParent(this Transform t)
    {
        return t.root != t ? t.root : null;
    }

    public static T GetComponentOrInChildren<T>(this Transform t)
    {
        T component = t.GetComponentInChildren<T>();
        if (!IsNull(component))
            return component;
        return t.GetComponentInChildren<T>();
    }
    
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        var result = aParent.Find(aName);
        if (result != null)
            return result;
        foreach (Transform child in aParent)
        {
            result = child.FindDeepChild(aName);
            if (result != null)
                return result;
        }
        return null;
    }

    public static IEnumerator MoveTo(this Transform t, Vector3 to, float duration)
    {
        return TransformUtils.MoveTo(t.position, to, duration, (newPosition) => t.position = newPosition);
    }

    public static T CachedComponent<T>(this Transform t, ref T backingField)
        where T : Component
    {
        if (backingField == null)
            backingField = t.GetComponent<T>();
        return backingField;
    }
}