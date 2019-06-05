using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PayloadedGameEventDeletor : EditorWindow
{
    string type;
    string pascalType
    {
        get
        {
            return char.ToUpper(type[0]) + type.Substring(1);
        }
    }

    [MenuItem("Custom/Event/Payloaded/Delete")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PayloadedGameEventDeletor));
    }

    void OnGUI()
    {
        GUILayout.Label("Delete payloaded game event and its listener", EditorStyles.boldLabel);

        GUILayout.Label("Type (int, PlayerData..)", EditorStyles.label);
        type = EditorGUILayout.TextField("", type, GUILayout.Width(300));

        if (GUILayout.Button("Delete", GUILayout.Width(150)))
        {
            if (type == "")
                return;
            
            string basicPath = Application.dataPath + "/Scripts/General/ScriptableObjects/GameEvent/Payloaded/" + pascalType + "/";
            string gameEventPath = basicPath + "GameEvent" + pascalType + ".cs";
            string gameEventListenerPath = basicPath + "GameEvent" + pascalType + "Listener.cs";
            string unityEventPath = Application.dataPath + "/Scripts/General/UnityEvents/UnityEvent" + pascalType + ".cs";

            DeleteFileAndMeta(gameEventPath);
            DeleteFileAndMeta(gameEventListenerPath);
            DeleteFileAndMeta(unityEventPath);
            DeleteFileAndMeta(Application.dataPath + "/Gizmos/GameEvent" + pascalType + " Icon.png");

            DeleteDirectoryAndMeta(basicPath);
            
            AssetDatabase.Refresh();
        }
    }

    void DeleteFileAndMeta(string path)
    {
        if (File.Exists(path))
            File.Delete(path);
        path += ".meta";
        if (File.Exists(path))
            File.Delete(path);
    }

    void DeleteDirectoryAndMeta(string path)
    {
        if (Directory.Exists(path))
            Directory.Delete(path);
        path = path.Remove(path.Length - 1);
        path += ".meta";
        if (File.Exists(path))
            File.Delete(path);
    }
}