using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraScalerAdapter : MonoBehaviour
{
    [SerializeField]
    CameraScalerDataVariable data;

    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Start()
    {
        DOVirtual.DelayedCall(.2f, UpdateValues);
    }

    [Button]
    public void UpdateValues()
    {
        CameraScalerData currentData = data.Value[ScalerDataUtils.GetCurrentScalerType()];
        if (currentData.useFov)
            DOVirtual.Float(cam.fieldOfView, currentData.fov, .15f, newValue => cam.fieldOfView = newValue).SetEase(Ease.InCirc);
        if (currentData.usePosition)
            transform.DOMove(currentData.position, .15f).SetEase(Ease.InCirc);
        if (currentData.useRotation)
            transform.DORotate(currentData.rotation, .15f).SetEase(Ease.InCirc);
    }
}