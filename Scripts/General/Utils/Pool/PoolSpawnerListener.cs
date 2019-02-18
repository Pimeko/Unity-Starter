using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolSpawnerListener : MonoBehaviour
{
    void Start()
    {
        PoolSpawner poolSpawner = GetComponent<PoolSpawner>();
        poolSpawner.AddListenerOnSpawn(OnSpawn);
    }

    protected abstract void OnSpawn(GameObject spawnedObject);
}