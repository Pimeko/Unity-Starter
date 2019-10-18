using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TransformExtensions
{
    private static bool IsNull<T>(T obj)
    {
        return EqualityComparer<T>.Default.Equals(obj, default(T));
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

    public static void Reset(this Transform t, bool local = true)
    {
        if (local)
        {
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
        }
        else
        {
            t.position = Vector3.zero;
            t.rotation = Quaternion.identity;
        }
        t.localScale = Vector3.one;
    }

    public static Transform[] FindChildren(this Transform transform, string name)
    {
        return transform.GetComponentsInChildren<Transform>().Where(t => t.name == name).ToArray();
    }
    
    /*
        Usage example:
        
        Rigidbody rb;
        Rigidbody Rb => transform.CachedComponent(ref rb);
     */
    public static T CachedComponent<T>(this Transform t, ref T backingField)
        where T : Component
    {
        if (backingField == null)
            backingField = t.GetComponent<T>();
        return backingField;
    }

    public static T CachedComponentInChildren<T>(this Transform t, ref T backingField)
        where T : Component
    {
        if (backingField == null)
            backingField = t.GetComponentInChildren<T>();
        return backingField;
    }

    public static T CachedComponentOrInChildren<T>(this Transform t, ref T backingField)
        where T : Component
    {
        if (backingField == null)
            backingField = t.GetComponentOrInChildren<T>();
        return backingField;
    }

    public static T[] CachedComponentsInChildren<T>(this Transform t, ref T[] backingField)
        where T : Component
    {
        if (backingField == null)
            backingField = t.GetComponentsInChildren<T>();
        return backingField;
    }

    public static T CachedComponentInParent<T>(this Transform t, ref T backingField)
        where T : Component
    {
        if (backingField == null)
            backingField = t.GetComponentInParent<T>();
        return backingField;
    }

    public static T[] GetComponentsInChildrenOnly<T>(this Transform t, bool includeInactive = false)
    {
        List<T> result = t.GetComponentsInChildren<T>(includeInactive).ToList();
        T parentComponent = t.GetComponent<T>();
        if (!IsNull(parentComponent))
            result.Remove(parentComponent);
        return result.ToArray();
    }
}