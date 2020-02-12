using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameObjectExtensions
{
    public static void ApplyOnChildrenRecursively(this GameObject gameObject, Action<GameObject> action)
    {
        if (gameObject == null)
            return;

        foreach (var trans in gameObject.GetComponentsInChildren<Transform>(true))
            action?.Invoke(trans.gameObject);
    }

    public static void ChangeLayerRecursively(this GameObject gameObject, int layer)
    {
        ApplyOnChildrenRecursively(gameObject, child => child.layer = layer);
    }

    public static void ChangeTagRecursively(this GameObject gameObject, string tag)
    {
        ApplyOnChildrenRecursively(gameObject, child => child.tag = tag);
    }

    public static string GetSceneFullPath(this GameObject gameObject)
    {
        string path = "/" + gameObject.name;
        while (gameObject.transform.parent != null)
        {
            gameObject = gameObject.transform.parent.gameObject;
            path = "/" + gameObject.name + path;
        }
        return path;
    }
}