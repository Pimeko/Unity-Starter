using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class VariableGenerator : EditorWindow
{
    string type;
    string pascalType
    {
        get
        {
            return char.ToUpper(type[0]) + type.Substring(1);
        }
    }
    bool isList;

    [MenuItem("Custom/Variable/Generate")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(VariableGenerator));
    }

    void OnGUI()
    {
        GUILayout.Label("Generate variable type", EditorStyles.boldLabel);

        GUILayout.Label("Type (int, bool..)", EditorStyles.label);
        type = EditorGUILayout.TextField("", type, GUILayout.Width(300));
        GUILayout.Label("Is a list", EditorStyles.label);
        isList = EditorGUILayout.Toggle("", isList);

        if (GUILayout.Button("Generate", GUILayout.Width(150)))
        {
            if (type == "")
                return;

            string pascalNameWithListVariable = pascalType + (isList ? "List" : "") + "Variable";

            string path = Application.dataPath + "/Scripts/General/ScriptableObjects/Variables/" +
                pascalType + "/" + pascalNameWithListVariable + ".cs";
            string templatePath = Application.dataPath + "/Editor/Variable/" + (isList ? "List" : "") + "VariableTemplate.txt";
            
            GenerateFile(path, templatePath);
            GenerateIcon(pascalNameWithListVariable);
            
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

    void GenerateIcon(string name)
    {
        File.Copy(
            Application.dataPath + "/Gizmos/IntVariable Icon.png",
            Application.dataPath + "/Gizmos/" + name + " Icon.png");
    }
}