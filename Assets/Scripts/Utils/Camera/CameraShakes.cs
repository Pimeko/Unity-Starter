using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraShakes : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    Vector3 currentInitialPosition;
    float currentFov;
    Sequence currentSequencePosition, currentSequenceFov;

    private void Start()
    {
        if (cam == null)
            cam = GetComponent<Camera>();

        currentSequencePosition = null;
        currentSequenceFov = null;
    }

    void InitPosition()
    {
        if (currentSequencePosition == null)
            currentInitialPosition = transform.localPosition;
    }

    void InitFov()
    {
        if (currentSequenceFov == null)
            currentFov = cam.fieldOfView;
    }

    [Button, TabGroup("Vertical")]
    public void SmallShakeVertical(Action callback)
    {
        InitPosition();
        
        currentSequencePosition = DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition + transform.up * 0.05f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.up * 0.05f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() =>
           {
               transform.localPosition = currentInitialPosition;
               currentSequencePosition = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Vertical")]
    public void ShakeVertical(Action callback)
    {
        InitPosition();
        
        currentSequencePosition = DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition + transform.up * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.up * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() =>
           {
               transform.localPosition = currentInitialPosition;
               currentSequencePosition = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Vertical")]
    public void LongShakeVertical(Action callback)
    {
        InitPosition();
        
        currentSequencePosition = DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition + transform.up * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.up * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition + transform.up * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.up * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() =>
           {
               transform.localPosition = currentInitialPosition;
               currentSequencePosition = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Horizontal")]
    public void VerySmallShakeHorizontal(Action callback)
    {
        InitPosition();
        
        currentSequencePosition = DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition + transform.right * .02f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.right * .02f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() =>
           {
               transform.localPosition = currentInitialPosition;
               currentSequencePosition = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Horizontal")]
    public void SmallShakeHorizontal(Action callback)
    {
        InitPosition();
        
        currentSequencePosition = DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition + transform.right * 0.05f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.right * 0.05f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() =>
           {
               transform.localPosition = currentInitialPosition;
               currentSequencePosition = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Horizontal")]
    public void MediumShakeHorizontal(Action callback)
    {
        InitPosition();
        
        currentSequencePosition = DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition + transform.right * 0.15f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.right * 0.15f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() =>
           {
               transform.localPosition = currentInitialPosition;
               currentSequencePosition = null;
               callback?.Invoke();
           })
            .SetUpdate(true);
    }

    [Button, TabGroup("Horizontal")]
    public void ShakeHorizontal(Action callback)
    {
        InitPosition();
        
        currentSequencePosition = DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition + transform.right * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.right * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() =>
           {
               transform.localPosition = currentInitialPosition;
               currentSequencePosition = null;
               callback?.Invoke();
           })
            .SetUpdate(true);
    }

    [Button, TabGroup("Horizontal")]
    public void LongShakeHorizontal(Action callback)
    {
        InitPosition();
        
        currentSequencePosition = DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition + transform.right * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.right * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition + transform.right * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.right * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() =>
           {
               transform.localPosition = currentInitialPosition;
               currentSequencePosition = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Zoom")]
    public void SmallZoomOutZoomIn(Action callback)
    {
        InitPosition();
        
        currentSequencePosition = DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition - transform.forward / 2, .15f))
           .Append(transform.DOLocalMove(transform.localPosition, .15f))
           .OnComplete(() =>
           {
               transform.localPosition = currentInitialPosition;
               currentSequencePosition = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Zoom")]
    public void ZoomOutZoomInElastic(Action callback)
    {
        InitPosition();
        
        currentSequencePosition = DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition - transform.forward, .5f))
           .Append(transform.DOLocalMove(transform.localPosition, .5f).SetEase(Ease.OutElastic))
           .OnComplete(() =>
           {
               transform.localPosition = currentInitialPosition;
               currentSequencePosition = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Zoom")]
    public void SmallZoomOutZoomInFov(Action callback)
    {
        InitFov();
        
        currentSequenceFov = DOTween.Sequence()
           .Append(DOVirtual.Float(currentFov, currentFov + 2, .15f, newFov => cam.fieldOfView = newFov))
           .Append(DOVirtual.Float(currentFov + 2, currentFov, .15f, newFov => cam.fieldOfView = newFov))
           .OnComplete(() =>
           {
               cam.fieldOfView = currentFov;
               currentSequenceFov = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Zoom")]
    public void ZoomInZoomOutFov(Action callback)
    {
        InitFov();
        
        currentSequenceFov = DOTween.Sequence()
           .Append(DOVirtual.Float(currentFov, currentFov - 5, 1, newFov => cam.fieldOfView = newFov))
           .Append(DOVirtual.Float(currentFov - 5, currentFov, .5f, newFov => cam.fieldOfView = newFov))
           .OnComplete(() =>
           {
               cam.fieldOfView = currentFov;
               currentSequenceFov = null;
               callback?.Invoke();
           });
    }

    [Button, TabGroup("Zoom")]
    public void ZoomOutZoomInElasticFov(Action callback)
    {
        InitFov();
        
        currentSequenceFov = DOTween.Sequence()
           .Append(DOVirtual.Float(currentFov, currentFov + 5, .5f, newFov => cam.fieldOfView = newFov))
           .Append(DOVirtual.Float(currentFov + 5, currentFov, .5f, newFov => cam.fieldOfView = newFov).SetEase(Ease.OutElastic))
           .OnComplete(() =>
           {
               cam.fieldOfView = currentFov;
               currentSequenceFov = null;
               callback?.Invoke();
           });
    }
}