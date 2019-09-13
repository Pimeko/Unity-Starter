using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;
    
    public void Spawn()
    {
        GameObject instance = Instantiate(prefab);
        instance.transform.SetParent(transform);
    }
}