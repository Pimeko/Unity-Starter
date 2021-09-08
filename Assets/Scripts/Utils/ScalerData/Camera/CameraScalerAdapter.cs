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
    [SerializeField]
    float transitionDuration = .15f;
    [SerializeField, InfoBox("Follow offset or Local Position")]
    bool useLocalPosition = false;

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
                transitionDuration,
                newValue => virtualCamera.m_Lens.FieldOfView = newValue).SetEase(Ease.InCirc);
        }
        if (currentData.usePosition)
        {
            if (useLocalPosition)
                transform.DOLocalMove(currentData.offsetPosition, transitionDuration).SetEase(Ease.InCirc);
            else
            {
                DOTween.To(
                    () => cinemachineTransposer.m_FollowOffset,
                    offset => cinemachineTransposer.m_FollowOffset = offset,
                    currentData.offsetPosition,
                    transitionDuration).SetEase(Ease.InCirc);
            }
        }
    }
}