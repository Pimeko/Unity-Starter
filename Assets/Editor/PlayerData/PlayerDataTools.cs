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
            Debug.Log("Deleted data file at " + path);
        }
        else
            Debug.Log("Could not delete data file: no file found.");
        
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Custom/PlayerData/Open in IDE %&a")]
    static void OpenInIDE()
    {
        UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(Application.persistentDataPath + "/data", 0);
    }
}