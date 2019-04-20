using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PayloadedGameEventGenerator : EditorWindow
{
    string type;
    string pascalType
    {
        get
        {
            return char.ToUpper(type[0]) + type.Substring(1);
        }
    }

    [MenuItem("Custom/Event/Payloaded/Generate")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PayloadedGameEventGenerator));
    }

    void OnGUI()
    {
        GUILayout.Label("Generate payloaded game event, listener and raiser", EditorStyles.boldLabel);

        GUILayout.Label("Type (int, PlayerData..)", EditorStyles.label);
        type = EditorGUILayout.TextField("", type, GUILayout.Width(300));

        if (GUILayout.Button("Generate", GUILayout.Width(150)))
        {
            if (type == "")
                return;
            
            string basicPath = Application.dataPath + "/Scripts/General/ScriptableObjects/GameEvent/Payloaded/" + pascalType + "/";
            string gameEventPath = basicPath + "GameEvent" + pascalType + ".cs";
            string gameEventListenerPath = basicPath + "GameEvent" + pascalType + "Listener.cs";
            string gameEventRaiserPath = basicPath + "GameEvent" + pascalType + "Raiser.cs";
            string unityEventPath = Application.dataPath + "/Scripts/General/UnityEvents/UnityEvent" + pascalType + ".cs";

            GenerateFile(gameEventPath, Application.dataPath + "/Editor/GameEvent/GameEventTemplate.txt");
            GenerateFile(gameEventListenerPath, Application.dataPath + "/Editor/GameEvent/GameEventListenerTemplate.txt");
            GenerateFile(gameEventRaiserPath, Application.dataPath + "/Editor/GameEvent/GameEventRaiserTemplate.txt");
            GenerateFile(unityEventPath, Application.dataPath + "/Editor/GameEvent/UnityEventTemplate.txt");
            
            AssetDatabase.Refresh();
        }
    }

    void GenerateFile(string filePath, string templatePath)
    {
        FileInfo file = new FileInfo(filePath);
        string templateAsString = File.ReadAllText(templatePath);
        string content = templateAsString.Replace("[PASCAL]", pascalType);
        content = content.Replace("[TYPE]", type);
        file.Directory.Create();
        File.WriteAllText(filePath, content);
    }
}