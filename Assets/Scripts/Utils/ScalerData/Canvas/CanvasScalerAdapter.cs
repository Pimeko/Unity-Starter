using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScalerAdapter : MonoBehaviour
{
    [SerializeField]
    CanvasScalerDataVariable data;

    CanvasScaler canvasScaler;

    void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();

        DOVirtual.DelayedCall(.2f, UpdateValues);
    }

    [Button]
    public void UpdateValues()
    {
        float currentData = data.Value[ScalerDataUtils.GetCurrentScalerType()];
        canvasScaler.matchWidthOrHeight = currentData;
    }
}