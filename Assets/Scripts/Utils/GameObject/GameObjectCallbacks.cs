using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectCallbacks : MonoBehaviour
{
    public Action<GameObject> onStart, onEnable, onDisable, onDestroy;

    void Start()
    {
        onStart?.Invoke(gameObject);
    }

    void OnEnable()
    {
        onEnable?.Invoke(gameObject);
    }

    void OnDisable()
    {
        onDisable?.Invoke(gameObject);
    }

    void OnDestroy()
    {
        onDestroy?.Invoke(gameObject);
    }
}