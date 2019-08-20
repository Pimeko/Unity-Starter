using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class DevTools : Editor
{
    [MenuItem("Custom/Shortcuts/Remove Copy Names &b")]
    private static void RemoveCopyNames()
    {
        GameObject[] objs = Selection.gameObjects;
        foreach (GameObject o in objs)
            o.name = o.name.Substring(0, o.name.Length - 4);
    }

	[MenuItem("Custom/Shortcuts/Clear console &x")]
    private static void ClearConsole()
    {
        var logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
 
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
 
        clearMethod.Invoke(null, null);
    }

	[MenuItem("Custom/Shortcuts/Pause Game %e")]
    private static void PauseTheGame()
    {
        EditorApplication.isPaused = !EditorApplication.isPaused;
    }

    [MenuItem("Custom/Shortcuts/Play &p")]
    private static void Play()
    {
        EditorSceneManager.OpenScene("Assets/_Scenes/Menu.unity");
        EditorApplication.isPlaying = true;
    }

    [MenuItem("Custom/Shortcuts/New folder &n")]
    private static void NewFolder()
    {
        EditorApplication.ExecuteMenuItem("Assets/Create/Folder");
    }

    [MenuItem("Custom/Shortcuts/Create container child &q")]
    private static void CreateContainerChild()
    {
        GameObject container = new GameObject("Container");
        container.transform.SetParent(Selection.activeGameObject.transform);
        container.transform.localPosition = Vector3.zero;
    }

    [MenuItem("Custom/Shortcuts/Create container and put all as children %q")]
    private static void CreateContainerAndPutAllAsChildren()
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in Selection.activeGameObject.transform)
            children.Add(child);

        GameObject container = new GameObject("Container");
        container.transform.SetParent(Selection.activeGameObject.transform);
        container.transform.localPosition = Vector3.zero;

        foreach (Transform child in children)
            child.SetParent(container.transform);
    }

    [MenuItem("Custom/Shortcuts/Reload current scene &r")]
    private static void ReloadCurrentScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    private static double startTime;
    [MenuItem("Custom/Shortcuts/Restart &t")]
    private static void Restart()
    {
        startTime = EditorApplication.timeSinceStartup;
        EditorApplication.isPlaying = false;

        EditorApplication.update += RestartUpdate;
    }

    private static void RestartUpdate()
    {
        if (EditorApplication.timeSinceStartup - startTime > 1)
        {
            EditorApplication.isPlaying = true;
            EditorApplication.update -= RestartUpdate;
        }
    }
}
