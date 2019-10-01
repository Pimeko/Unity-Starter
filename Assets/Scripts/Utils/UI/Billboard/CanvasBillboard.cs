using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CanvasBillboard : MonoBehaviour
{
    [SerializeField]
    GameObjectVariable currentCameraGO;
    [SerializeField]
    bool billboardOnUpdate;

    Camera cam;

    void Start()
    {
        cam = currentCameraGO.Value.GetComponent<Camera>();
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