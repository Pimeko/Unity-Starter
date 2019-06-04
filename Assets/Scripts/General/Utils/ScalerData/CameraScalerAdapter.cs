using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScalerAdapter : MonoBehaviour
{
    [SerializeField]
    ScalerDataVariable data;

    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    [ExecuteInEditMode]
    void OnEnable()
    {
        cam.fieldOfView = data.GetCurrentMatch();
    }
}