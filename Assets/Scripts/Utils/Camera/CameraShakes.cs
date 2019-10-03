using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShakes : MonoBehaviour
{
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    [Button, TabGroup("Vertical")]
    public void SmallShakeVertical(Action callback)
    {
        Vector3 initialPosition = transform.position;
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position + transform.up * 0.05f, .1f))
           .Append(transform.DOMove(transform.position - transform.up * 0.05f, .1f))
           .Append(transform.DOMove(transform.position, .1f))
           .OnComplete(() => { transform.position = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Vertical")]
    public void ShakeVertical(Action callback)
    {
        Vector3 initialPosition = transform.position;
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position + transform.up * 0.2f, .1f))
           .Append(transform.DOMove(transform.position - transform.up * 0.2f, .1f))
           .Append(transform.DOMove(transform.position, .1f))
           .OnComplete(() => { transform.position = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Vertical")]
    public void LongShakeVertical(Action callback)
    {
        Vector3 initialPosition = transform.position;
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position + transform.up * 0.2f, .1f))
           .Append(transform.DOMove(transform.position - transform.up * 0.2f, .1f))
           .Append(transform.DOMove(transform.position + transform.up * 0.2f, .1f))
           .Append(transform.DOMove(transform.position - transform.up * 0.2f, .1f))
           .Append(transform.DOMove(transform.position, .1f))
           .OnComplete(() => { transform.position = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Horizontal")]
    public void VerySmallShakeHorizontal(Action callback)
    {
        Vector3 initialPosition = transform.position;
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position + transform.right * .02f, .1f))
           .Append(transform.DOMove(transform.position - transform.right * .02f, .1f))
           .Append(transform.DOMove(transform.position, .1f))
           .OnComplete(() => { transform.position = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Horizontal")]
    public void SmallShakeHorizontal(Action callback)
    {
        Vector3 initialPosition = transform.position;
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position + transform.right * 0.05f, .1f))
           .Append(transform.DOMove(transform.position - transform.right * 0.05f, .1f))
           .Append(transform.DOMove(transform.position, .1f))
           .OnComplete(() => { transform.position = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Horizontal")]
    public void ShakeHorizontal(Action callback)
    {
        Vector3 initialPosition = transform.position;
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position + transform.right * 0.2f, .1f))
           .Append(transform.DOMove(transform.position - transform.right * 0.2f, .1f))
           .Append(transform.DOMove(transform.position, .1f))
           .OnComplete(() => { transform.position = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Horizontal")]
    public void LongShakeHorizontal(Action callback)
    {
        Vector3 initialPosition = transform.position;
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position + transform.right * 0.2f, .1f))
           .Append(transform.DOMove(transform.position - transform.right * 0.2f, .1f))
           .Append(transform.DOMove(transform.position + transform.right * 0.2f, .1f))
           .Append(transform.DOMove(transform.position - transform.right * 0.2f, .1f))
           .Append(transform.DOMove(transform.position, .1f))
           .OnComplete(() => { transform.position = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Zoom")]
    public void SmallZoomOutZoomIn(Action callback)
    {
        Vector3 initialPosition = transform.position;
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position - transform.forward / 2, .15f))
           .Append(transform.DOMove(transform.position, .15f))
           .OnComplete(() => { transform.position = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Zoom")]
    public void ZoomOutZoomInElastic(Action callback)
    {
        Vector3 initialPosition = transform.position;
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position - transform.forward, .5f))
           .Append(transform.DOMove(transform.position, .5f).SetEase(Ease.OutElastic))
           .OnComplete(() => { transform.position = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Zoom")]
    public void SmallZoomOutZoomInFov(Action callback)
    {
        float initialFov = cam.fieldOfView;
        DOTween.Sequence()
           .Append(DOVirtual.Float(initialFov, initialFov + 2, .15f, newFov => cam.fieldOfView = newFov))
           .Append(DOVirtual.Float(initialFov + 2, initialFov, .15f, newFov => cam.fieldOfView = newFov))
           .OnComplete(() => { cam.fieldOfView = initialFov; callback?.Invoke(); });
    }

    [Button, TabGroup("Zoom")]
    public void ZoomInZoomOutFov(Action callback)
    {
        float initialFov = cam.fieldOfView;
        DOTween.Sequence()
           .Append(DOVirtual.Float(initialFov, initialFov - 5, 1, newFov => cam.fieldOfView = newFov))
           .Append(DOVirtual.Float(initialFov - 5, initialFov, .5f, newFov => cam.fieldOfView = newFov))
           .OnComplete(() => { cam.fieldOfView = initialFov; callback?.Invoke(); });
    }

    [Button, TabGroup("Zoom")]
    public void ZoomOutZoomInElasticFov(Action callback)
    {
        float initialFov = cam.fieldOfView;
        DOTween.Sequence()
           .Append(DOVirtual.Float(initialFov, initialFov + 5, .5f, newFov => cam.fieldOfView = newFov))
           .Append(DOVirtual.Float(initialFov + 5, initialFov, .5f, newFov => cam.fieldOfView = newFov).SetEase(Ease.OutElastic))
           .OnComplete(() => { cam.fieldOfView = initialFov; callback?.Invoke(); });
    }
}