using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraShakes : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    CinemachineBrain cinemachineBrain;
    CinemachineVirtualCamera currentVCam { get { return (CinemachineVirtualCamera)cinemachineBrain.ActiveVirtualCamera; } }

    Vector3 currentInitialPosition;
    float currentFov;
    Sequence currentSequencePosition, currentSequenceFov;

    void Start()
    {
        if (cam == null)
            cam = GetComponent<Camera>();

        cinemachineBrain = GetComponent<CinemachineBrain>();

        currentSequencePosition = null;
        currentSequenceFov = null;
    }

    #region Position

    void InitPosition()
    {
        if (currentSequencePosition == null)
            currentInitialPosition = currentVCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    Tween CameraLocalMove(Vector3 offset, float time, Ease? ease = null)
    {
        CinemachineTransposer t = currentVCam.GetCinemachineComponent<CinemachineTransposer>();

        return DOTween.To(
            () => t.m_FollowOffset,
            value => t.m_FollowOffset = value,
            currentInitialPosition + offset,
            time
        ).SetEase((Ease)(ease == null ? Ease.Linear : ease));
    }

    #region vertical

    [Button, TabGroup("Vertical")]
    public void SmallShakeVertical(Action callback)
    {
        InitPosition();

        currentSequencePosition = DOTween.Sequence()
           .Append(CameraLocalMove(currentVCam.transform.up * .1f, .1f))
           .Append(CameraLocalMove(-currentVCam.transform.up * .1f, .1f))
           .Append(CameraLocalMove(Vector3.zero, .1f))
           .OnComplete(() =>
           {
               currentSequencePosition = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Vertical")]
    public void ShakeVertical(Action callback)
    {
        InitPosition();

        currentSequencePosition = DOTween.Sequence()
           .Append(CameraLocalMove(currentVCam.transform.up * .3f, .1f))
           .Append(CameraLocalMove(-currentVCam.transform.up * .3f, .1f))
           .Append(CameraLocalMove(Vector3.zero, .1f))
           .OnComplete(() =>
           {
               currentSequencePosition = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Vertical")]
    public void LongShakeVertical(Action callback)
    {
        InitPosition();

        currentSequencePosition = DOTween.Sequence()
           .Append(CameraLocalMove(currentVCam.transform.up * .2f, .1f))
           .Append(CameraLocalMove(-currentVCam.transform.up * .2f, .1f))
           .Append(CameraLocalMove(currentVCam.transform.up * .2f, .1f))
           .Append(CameraLocalMove(-currentVCam.transform.up * .2f, .1f))
           .Append(CameraLocalMove(Vector3.zero, .1f))
           .OnComplete(() =>
           {
               currentSequencePosition = null;
               callback?.Invoke();
           });
    }
    #endregion

    #region horizontal

    [Button, TabGroup("Horizontal")]
    public void SmallShakeHorizontal(Action callback)
    {
        InitPosition();

        currentSequencePosition = DOTween.Sequence()
           .Append(CameraLocalMove(currentVCam.transform.right * .05f, .1f))
           .Append(CameraLocalMove(-currentVCam.transform.right * .05f, .1f))
           .Append(CameraLocalMove(Vector3.zero, .1f))
           .OnComplete(() =>
           {
               currentSequencePosition = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Horizontal")]
    public void MediumShakeHorizontal(Action callback)
    {
        InitPosition();

        currentSequencePosition = DOTween.Sequence()
           .Append(CameraLocalMove(currentVCam.transform.right * .15f, .1f))
           .Append(CameraLocalMove(-currentVCam.transform.right * .15f, .1f))
           .Append(CameraLocalMove(Vector3.zero, .1f))
           .OnComplete(() =>
           {
               currentSequencePosition = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Horizontal")]
    public void ShakeHorizontal(Action callback)
    {
        InitPosition();

        currentSequencePosition = DOTween.Sequence()
           .Append(CameraLocalMove(currentVCam.transform.right * .2f, .1f))
           .Append(CameraLocalMove(-currentVCam.transform.right * .2f, .1f))
           .Append(CameraLocalMove(Vector3.zero, .1f))
           .OnComplete(() =>
           {
               currentSequencePosition = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Horizontal")]
    public void LongShakeHorizontal(Action callback)
    {
        InitPosition();

        currentSequencePosition = DOTween.Sequence()
           .Append(CameraLocalMove(currentVCam.transform.right * .2f, .1f))
           .Append(CameraLocalMove(-currentVCam.transform.right * .2f, .1f))
           .Append(CameraLocalMove(currentVCam.transform.right * .2f, .1f))
           .Append(CameraLocalMove(-currentVCam.transform.right * .2f, .1f))
           .Append(CameraLocalMove(Vector3.zero, .1f))
           .OnComplete(() =>
           {
               currentSequencePosition = null;
               callback?.Invoke();
           });
    }
    #endregion

    #endregion

    # region FOV

    void InitFov()
    {
        if (currentSequenceFov == null)
            currentFov = currentVCam.m_Lens.FieldOfView;
    }

    Tween CameraFov(float offset, float time, Ease? ease = null)
    {
        return DOTween.To(
            () => currentVCam.m_Lens.FieldOfView,
            value => currentVCam.m_Lens.FieldOfView = value,
            currentVCam.m_Lens.FieldOfView + offset,
            time
        ).SetEase((Ease)(ease == null ? Ease.Linear : ease));
    }

    #region Zoom Out - Zoom In

    [Button, TabGroup("Zoom")]
    public void SmallZoomOutZoomIn(Action callback)
    {
        InitFov();

        currentSequenceFov = DOTween.Sequence()
           .Append(CameraFov(2, .15f))
           .Append(CameraFov(0, .15f))
           .OnComplete(() =>
           {
               currentVCam.m_Lens.FieldOfView = currentFov;
               currentSequenceFov = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Zoom")]
    public void ZoomOutZoomIn(Action callback)
    {
        InitFov();

        currentSequenceFov = DOTween.Sequence()
           .Append(CameraFov(5, .3f))
           .Append(CameraFov(0, .3f))
           .OnComplete(() =>
           {
               currentVCam.m_Lens.FieldOfView = currentFov;
               currentSequenceFov = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Zoom")]
    public void ZoomOutZoomInElastic(Action callback)
    {
        InitFov();

        currentSequenceFov = DOTween.Sequence()
           .Append(CameraFov(2, .5f))
           .Append(CameraFov(0, .5f, Ease.OutElastic))
           .OnComplete(() =>
           {
               currentVCam.m_Lens.FieldOfView = currentFov;
               currentSequenceFov = null;
               callback?.Invoke();
           });
    }
    #endregion

    #region Zoom In - Zoom Out

    [Button, TabGroup("Zoom")]
    public void SmallZoomInZoomOut(Action callback)
    {
        InitFov();

        currentSequenceFov = DOTween.Sequence()
           .Append(CameraFov(-2, .15f))
           .Append(CameraFov(0, .15f))
           .OnComplete(() =>
           {
               currentVCam.m_Lens.FieldOfView = currentFov;
               currentSequenceFov = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Zoom")]
    public void ZoomInZoomOut(Action callback)
    {
        InitFov();

        currentSequenceFov = DOTween.Sequence()
           .Append(CameraFov(-5, .3f))
           .Append(CameraFov(0, .3f))
           .OnComplete(() =>
           {
               currentVCam.m_Lens.FieldOfView = currentFov;
               currentSequenceFov = null;
               callback?.Invoke();
           });
    }
    #endregion

    #endregion
}