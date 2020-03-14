using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class TransformExtensions
{
    # region Cached
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

    public static List<T> CachedComponentsInChildrenList<T>(this Transform t, ref List<T> backingField, bool includeInactive = false)
        where T : Component
    {
        if (backingField == null)
            backingField = t.GetComponentsInChildren<T>(includeInactive).ToList();
        return backingField;
    }

    public static T CachedComponentInParent<T>(this Transform t, ref T backingField)
        where T : Component
    {
        if (backingField == null)
            backingField = t.GetComponentInParent<T>();
        return backingField;
    }
    # endregion

    public static T[] GetComponentsInChildrenOnly<T>(this Transform t, bool includeInactive = false)
    {
        List<T> result = t.GetComponentsInChildren<T>(includeInactive).ToList();
        T parentComponent = t.GetComponent<T>();
        if (!IsNull(parentComponent))
            result.Remove(parentComponent);
        return result.ToArray();
    }

    public static T[] GetComponentsInFirstChildrenOnly<T>(this Transform t, bool includeInactive = false)
    {
        List<T> result = new List<T>();
        foreach (Transform child in t)
        {
            T component;
            if ((child.gameObject.activeInHierarchy || includeInactive) && (component = child.GetComponent<T>()) != null)
                result.Add(component);
        }
        return result.ToArray();
    }

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

    public static void DestroyAllChildren(this Transform transform)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
                Object.Destroy(transform.GetChild(i).gameObject);
            else
                Object.DestroyImmediate(transform.GetChild(i).gameObject);
#else
            Object.Destroy(transform.GetChild(i).gameObject);
#endif
        }
    }

    public static Transform[] GetChildren(this Transform transform, bool includeInactive = false)
    {
        return transform.GetComponentsInChildrenOnly<Transform>(includeInactive);
    }

    public static Transform[] GetFirstChildren(this Transform transform, bool includeInactive = false)
    {
        return transform.GetComponentsInFirstChildrenOnly<Transform>(includeInactive);
    }

    public static RectTransform[] GetChildren(this RectTransform transform, bool includeInactive = false)
    {
        return transform.GetComponentsInChildrenOnly<RectTransform>(includeInactive);
    }

    public static RectTransform[] GetFirstChildren(this RectTransform transform, bool includeInactive = false)
    {
        return transform.GetComponentsInFirstChildrenOnly<RectTransform>(includeInactive);
    }

    public static void SetPositionX(this Transform transform, float value, bool local = false)
    {
        if (local)
            transform.localPosition = new Vector3(
                value,
                transform.localPosition.y,
                transform.localPosition.z
            );
        else
            transform.position = new Vector3(
                value,
                transform.position.y,
                transform.position.z
            );
    }

    public static void SetPositionY(this Transform transform, float value, bool local = false)
    {
        if (local)
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                value,
                transform.localPosition.z
            );
        else
            transform.position = new Vector3(
                transform.position.x,
                value,
                transform.position.z
            );
    }

    public static void SetPositionZ(this Transform transform, float value, bool local = false)
    {
        if (local)
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                transform.localPosition.y,
                value
            );
        else
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                value
            );
    }

    public static void SetLocalScaleX(this Transform transform, float value)
    {
        Vector3 newScale = new Vector3(
                value,
                transform.localScale.y,
                transform.localScale.z
            );
        transform.localScale = newScale;
    }

    public static void SetLocalScaleY(this Transform transform, float value)
    {
        Vector3 newScale = new Vector3(
                transform.localScale.x,
                value,
                transform.localScale.z
            );
        transform.localScale = newScale;
    }

    public static void SetLocalScaleZ(this Transform transform, float value)
    {
        Vector3 newScale = new Vector3(
                transform.localScale.x,
                transform.localScale.y,
                value
            );
        transform.localScale = newScale;
    }
}