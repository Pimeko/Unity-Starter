using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PlayerDataVariableGenerator : EditorWindow
{
    string variableName, serializedType, soType, defaultValue;
    string variableNamePascal
    {
        get
        {
            return char.ToUpper(variableName[0]) + variableName.Substring(1);
        }
    }

    [MenuItem("Custom/PlayerData/Generate variable")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PlayerDataVariableGenerator));
    }

    void OnGUI()
    {
        GUILayout.Label("Generate player data's variable", EditorStyles.boldLabel);

        GUILayout.Label("Name (bestScore, nbCoins...)", EditorStyles.label);
        variableName = EditorGUILayout.TextField("", variableName, GUILayout.Width(300));

        GUILayout.Label("Serialized type (int, bool...)", EditorStyles.label);
        serializedType = EditorGUILayout.TextField("", serializedType, GUILayout.Width(300));

        GUILayout.Label("Scriptable Object type (IntVariable, BoolVariable...)", EditorStyles.label);
        soType = EditorGUILayout.TextField("", soType, GUILayout.Width(300));

        GUILayout.Label("Default value (0, false...)", EditorStyles.label);
        defaultValue = EditorGUILayout.TextField("", defaultValue, GUILayout.Width(300));

        if (GUILayout.Button("Generate", GUILayout.Width(150)))
        {
            if (variableName == "" || serializedType == null|| soType == null || defaultValue == null)
                return;

            string filePath = Application.dataPath + "/Scripts/General/Utils/PlayerData/Variables/PlayerData" + variableNamePascal + ".cs";
            string templatePath = Application.dataPath + "/Editor/PlayerData/PlayerDataVariableTemplate.txt";
            GenerateFile(filePath, templatePath);

            ScriptableObject so = ScriptableObject.CreateInstance(soType);
            string soPath = "Assets/ScriptableObjects/PlayerData/Variables/";
            
            if (!Directory.Exists(soPath))
                Directory.CreateDirectory(soPath);
            AssetDatabase.CreateAsset(so, soPath + variableNamePascal + ".asset");
            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();
        }
    }

    void GenerateFile(string filePath, string templatePath)
    {
        FileInfo file = new FileInfo(filePath);
        string templateAsString = File.ReadAllText(templatePath);
        string content = templateAsString.Replace("[NAME]", variableName);
        content = content.Replace("[PASCAL_NAME]", variableNamePascal);
        content = content.Replace("[TYPE]", serializedType);
        content = content.Replace("[SO_TYPE]", soType);
        content = content.Replace("[DEFAULT_VALUE]", defaultValue);
        file.Directory.Create();
        File.WriteAllText(filePath, content);
    }
}