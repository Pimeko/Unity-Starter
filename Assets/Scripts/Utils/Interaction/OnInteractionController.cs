using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public abstract class InteractionAction<T_UNITY_EVENT>
{
    [SerializeField, ReorderableList]
    List<string> tags;
    public List<string> Tags { get { return tags; } }

    [SerializeField]
    T_UNITY_EVENT onInteraction;
    public T_UNITY_EVENT OnInteraction { get { return onInteraction; } }
}

public abstract class OnInteractionController<T_INTERACTION, T_UNITY_EVENT> : MonoBehaviour
    where T_INTERACTION : InteractionAction<T_UNITY_EVENT>
{
    [SerializeField]
    protected bool printDebug = false;
    [SerializeField, ReorderableList]
    List<T_INTERACTION> interactions;

    protected Dictionary<string, T_INTERACTION> actions;

    void Start()
    {
        InitActions();
    }
    
    void InitActions()
    {
        actions = new Dictionary<string, T_INTERACTION>();
        foreach (T_INTERACTION action in interactions)
        {
            foreach (string tag in action.Tags)
                actions.Add(tag, action);
        }
    }
}