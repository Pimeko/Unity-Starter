using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolSpawnerListener : MonoBehaviour
{
    PoolSpawner poolSpawner;

    void Start()
    {
        poolSpawner = GetComponent<PoolSpawner>();
        
        poolSpawner.OnSpawn += OnSpawn;
    }

    void OnDestroy()
    {
        poolSpawner.OnSpawn -= OnSpawn;
    }

    protected abstract void OnSpawn(GameObject spawnedObject);
}