using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

// makes sure that the static constructor is always called in the editor.
[InitializeOnLoad]
public class DraggableBasicGameEventEditor : Editor
{
    // static DraggableBasicGameEventEditor()
    // {
    //     // Adds a callback for when the hierarchy window processes GUI events
    //     // for every GameObject in the heirarchy.
    //     EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemCallback;
    // }

    // static void HierarchyWindowItemCallback(int instanceID, Rect pRect)
    // {
    //     // happens when an acceptable item is released over the GUI window
    //     if (Event.current.type == EventType.DragPerform && pRect.Contains(Event.current.mousePosition))
    //     {
    //         // get all the drag and drop information ready for processing.
    //         DragAndDrop.AcceptDrag();

    //         GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

    //         if (gameObject == null)
    //             return;

    //         // run through each object that was dragged in.
    //         foreach (var objectRef in DragAndDrop.objectReferences)
    //         {
    //             // if the object is the particular asset type...
    //             if (objectRef is BasicGameEvent)
    //             {
    //                 var listener = gameObject.AddComponent<BasicGameEventListener>();
    //                 listener.AddGameEvent((BasicGameEvent)objectRef);
    //             }
    //         }

    //         Event.current.Use();
    //     }
    // }
}