using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PlayerDataTools : Editor
{
    [MenuItem("Custom/PlayerData/Reset &a")]
    static void Reset()
    {
        var path = Path.Combine(Application.persistentDataPath, "data");
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Deleted data files at " + path);
        }
        else
            Debug.Log("No data found.");
    }
    
    [MenuItem("Custom/PlayerData/Show in explorer")]
    static void ShowInExplorer()
    {
         Application.OpenURL("file://" + Path.Combine(Application.persistentDataPath));
    }
}