using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventCollision : UnityEvent<Collision> { }

[System.Serializable]
public class UnityEventCollider : UnityEvent<Collider> { }

public enum InteractionType
{
    ENTER,
    STAY,
    EXIT,
}

public class InteractionDetector : SerializedMonoBehaviour
{
    [SerializeField]
    Dictionary<string, Dictionary<InteractionType, UnityEvent<Collider>>> triggersCallbacks;
    [SerializeField]
    Dictionary<string, Dictionary<InteractionType, UnityEvent<Collision>>> collisionsCallbacks;

    public void AddTriggerCallback(string tag, InteractionType type, List<UnityAction<Collider>> callbacks)
    {
        if (triggersCallbacks == null)
            triggersCallbacks = new Dictionary<string, Dictionary<InteractionType, UnityEvent<Collider>>>();

        if (triggersCallbacks.ContainsKey(tag))
        {
            if (triggersCallbacks[tag].ContainsKey(type))
            {
                foreach (var callback in callbacks)
                    triggersCallbacks[tag][type].AddListener(callback);
            }
            else
            {
                UnityEvent<Collider> unityEvent = new UnityEventCollider();
                foreach (var callback in callbacks)
                    unityEvent.AddListener(callback);
                triggersCallbacks[tag].Add(type, unityEvent);
            }
        }
        else
        {
            UnityEvent<Collider> unityEvent = new UnityEventCollider();
            foreach (var callback in callbacks)
                unityEvent.AddListener(callback);
            Dictionary<InteractionType, UnityEvent<Collider>> dictionary = new Dictionary<InteractionType, UnityEvent<Collider>>();
            dictionary.Add(type, unityEvent);
            triggersCallbacks.Add(tag, dictionary);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggersCallbacks != null)
        {
            if (triggersCallbacks.ContainsKey(other.tag) && triggersCallbacks[other.tag].ContainsKey(InteractionType.ENTER))
                triggersCallbacks[other.tag][InteractionType.ENTER]?.Invoke(other);
            if (triggersCallbacks.ContainsKey("_") && triggersCallbacks["_"].ContainsKey(InteractionType.ENTER))
                triggersCallbacks["_"][InteractionType.ENTER]?.Invoke(other);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (triggersCallbacks != null)
        {
            if (triggersCallbacks.ContainsKey(other.tag) && triggersCallbacks[other.tag].ContainsKey(InteractionType.STAY))
                triggersCallbacks[other.tag][InteractionType.STAY]?.Invoke(other);
            if (triggersCallbacks.ContainsKey("_") && triggersCallbacks["_"].ContainsKey(InteractionType.STAY))
                triggersCallbacks["_"][InteractionType.STAY]?.Invoke(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (triggersCallbacks != null)
        {
            if (triggersCallbacks.ContainsKey(other.tag) && triggersCallbacks[other.tag].ContainsKey(InteractionType.EXIT))
                triggersCallbacks[other.tag][InteractionType.EXIT]?.Invoke(other);
            if (triggersCallbacks.ContainsKey("_") && triggersCallbacks["_"].ContainsKey(InteractionType.EXIT))
                triggersCallbacks["_"][InteractionType.EXIT]?.Invoke(other);
        }
    }

    public void AddCollisionCallback(string tag, InteractionType type, List<UnityAction<Collision>> callbacks)
    {
        if (collisionsCallbacks == null)
            collisionsCallbacks = new Dictionary<string, Dictionary<InteractionType, UnityEvent<Collision>>>();

        if (collisionsCallbacks.ContainsKey(tag))
        {
            if (collisionsCallbacks[tag].ContainsKey(type))
            {
                foreach (var callback in callbacks)
                    collisionsCallbacks[tag][type].AddListener(callback);
            }
            else
            {
                UnityEvent<Collision> unityEvent = new UnityEventCollision();
                foreach (var callback in callbacks)
                    unityEvent.AddListener(callback);
                collisionsCallbacks[tag].Add(type, unityEvent);
            }
        }
        else
        {
            UnityEvent<Collision> unityEvent = new UnityEventCollision();
            foreach (var callback in callbacks)
                unityEvent.AddListener(callback);
            Dictionary<InteractionType, UnityEvent<Collision>> dictionary = new Dictionary<InteractionType, UnityEvent<Collision>>();
            dictionary.Add(type, unityEvent);
            collisionsCallbacks.Add(tag, dictionary);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        var tag = other.collider.tag;
        if (collisionsCallbacks != null)
        {
            if (collisionsCallbacks.ContainsKey(tag) && collisionsCallbacks[tag].ContainsKey(InteractionType.ENTER))
                collisionsCallbacks[tag][InteractionType.ENTER]?.Invoke(other);
            if (collisionsCallbacks.ContainsKey("_") && collisionsCallbacks["_"].ContainsKey(InteractionType.ENTER))
                collisionsCallbacks["_"][InteractionType.ENTER]?.Invoke(other);
        }
    }

    void OnCollisionStay(Collision other)
    {
        var tag = other.collider.tag;
        if (collisionsCallbacks != null)
        {
            if (collisionsCallbacks.ContainsKey(tag) && collisionsCallbacks[tag].ContainsKey(InteractionType.STAY))
                collisionsCallbacks[tag][InteractionType.STAY]?.Invoke(other);
            if (collisionsCallbacks.ContainsKey("_") && collisionsCallbacks["_"].ContainsKey(InteractionType.STAY))
                collisionsCallbacks["_"][InteractionType.STAY]?.Invoke(other);
        }
    }

    void OnCollisionExit(Collision other)
    {
        var tag = other.collider.tag;
        if (collisionsCallbacks != null)
        {
            if (collisionsCallbacks.ContainsKey(tag) && collisionsCallbacks[tag].ContainsKey(InteractionType.EXIT))
                collisionsCallbacks[tag][InteractionType.EXIT]?.Invoke(other);
            if (collisionsCallbacks.ContainsKey("_") && collisionsCallbacks["_"].ContainsKey(InteractionType.EXIT))
                collisionsCallbacks["_"][InteractionType.EXIT]?.Invoke(other);
        }
    }
}