using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Cinemachine;

public class CameraScalerAdapter : MonoBehaviour
{
    [SerializeField]
    CameraScalerDataVariable data;

    CinemachineVirtualCamera virtualCamera;
    CinemachineTransposer cinemachineTransposer;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        UpdateValues();
    }

    [Button]
    public void UpdateValues()
    {
        var type = ScalerDataUtils.GetCurrentScalerType();
        CameraScalerData currentData = data.Value[type];
        if (currentData.useFov)
        {
            DOVirtual.Float(
                virtualCamera.m_Lens.FieldOfView,
                currentData.fov,
                .15f,
                newValue => virtualCamera.m_Lens.FieldOfView = newValue).SetEase(Ease.InCirc);
        }
        if (currentData.usePosition)
        {
            DOTween.To(
                () => cinemachineTransposer.m_FollowOffset,
                offset => cinemachineTransposer.m_FollowOffset = offset,
                currentData.offsetPosition,
                .15f).SetEase(Ease.InCirc);
        }
    }
}