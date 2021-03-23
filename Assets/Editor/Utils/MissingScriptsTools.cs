using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MissingScriptsTools : Editor
{
    static int go_count = 0, components_count = 0, missing_count = 0;

    [MenuItem("Custom/Delete Missing Scripts")]
    static void DeleteMissingScripts(MenuCommand command)
    {
        GameObject[] go = Selection.gameObjects;
        go_count = 0;
        components_count = 0;
        missing_count = 0;

        foreach (GameObject g in go)
            FindInGO(g);
        Debug.Log("Deleted " + missing_count + " missing scripts");
    }

    private static void FindInGO(GameObject g)
    {
        go_count++;
        Component[] components = g.GetComponents<Component>();
        for (int i = 0; i < components.Length; i++)
        {
            components_count++;
            if (components[i] == null)
            {
                missing_count++;
                string s = g.name;
                Transform t = g.transform;
                while (t.parent != null)
                {
                    s = t.parent.name + "/" + s;
                    t = t.parent;
                }
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(g);
            }
        }
        // Now recurse through each child GO (if there are any):
        foreach (Transform childT in g.transform)
        {
            //Debug.Log("Searching " + childT.name  + " " );
            FindInGO(childT.gameObject);
        }
    }
}