using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpController : MonoBehaviour
{
    [SerializeField]
    GameObjectVariable dumpGO;

    void OnEnable()
    {
        dumpGO.Value = gameObject;    
    }

    public void Restart()
    {
        transform.DestroyAllChildren();
    }
}