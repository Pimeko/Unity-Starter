using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CanvasBillboard : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    [SerializeField]
    bool billboardOnUpdate;

    void Start()
    {
        DoBillboard();
    }

    void Update()
    {
        if (billboardOnUpdate)
            DoBillboard();
    }

    [Button]
    void DoBillboard()
    {
        transform.forward = -cam.transform.forward;
    }
}