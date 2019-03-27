using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScalerAdapter : MonoBehaviour
{
    [SerializeField]
    CanvasScalerDataVariable data;

    CanvasScaler canvasScaler;

    void Awake()
    {
        canvasScaler = GetComponent<CanvasScaler>();
    }

    [ExecuteInEditMode]
    void OnEnable()
    {
        canvasScaler.matchWidthOrHeight = data.GetCurrentMatch();
    }
}