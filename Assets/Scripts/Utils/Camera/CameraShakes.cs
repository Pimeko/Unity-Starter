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
    public void ShakeHorizontalTweak(Action callback, float intensity = 1, float duration = 0.3f)
    {
        float factor = Mathf.Clamp(intensity, 0, 1) * 0.25f;
        Vector3 initialPosition = transform.localPosition;
        DOTween.Sequence()
            .AppendCallback(() =>
            {
                float elapsedTime = 0;
                while (elapsedTime < duration)
                {
                    DOTween.Sequence()
                    .Append(transform.DOLocalMove(transform.localPosition + transform.right * factor, .1f))
                    .Append(transform.DOLocalMove(transform.localPosition + transform.right * factor, .1f))
                    .AppendCallback(() => { elapsedTime += .2f; });
                }
            })
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() => { transform.localPosition = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Vertical")]
    public void SmallShakeVertical(Action callback)
    {
        Vector3 initialPosition = transform.localPosition;
        DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition + transform.up * 0.05f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.up * 0.05f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() => { transform.localPosition = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Vertical")]
    public void ShakeVertical(Action callback)
    {
        Vector3 initialPosition = transform.localPosition;
        DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition + transform.up * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.up * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() => { transform.localPosition = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Vertical")]
    public void LongShakeVertical(Action callback)
    {
        Vector3 initialPosition = transform.localPosition;
        DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition + transform.up * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.up * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition + transform.up * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.up * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() => { transform.localPosition = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Horizontal")]
    public void VerySmallShakeHorizontal(Action callback)
    {
        Vector3 initialPosition = transform.localPosition;
        DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition + transform.right * .02f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.right * .02f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() => { transform.localPosition = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Horizontal")]
    public void SmallShakeHorizontal(Action callback)
    {
        Vector3 initialPosition = transform.localPosition;
        DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition + transform.right * 0.05f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.right * 0.05f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() => { transform.localPosition = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Horizontal")]
    public void ShakeHorizontal(Action callback)
    {
        Vector3 initialPosition = transform.localPosition;
        DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition + transform.right * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.right * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() => { transform.localPosition = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Horizontal")]
    public void LongShakeHorizontal(Action callback)
    {
        Vector3 initialPosition = transform.localPosition;
        DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition + transform.right * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.right * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition + transform.right * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition - transform.right * 0.2f, .1f))
           .Append(transform.DOLocalMove(transform.localPosition, .1f))
           .OnComplete(() => { transform.localPosition = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Zoom")]
    public void SmallZoomOutZoomIn(Action callback)
    {
        Vector3 initialPosition = transform.localPosition;
        DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition - transform.forward / 2, .15f))
           .Append(transform.DOLocalMove(transform.localPosition, .15f))
           .OnComplete(() => { transform.localPosition = initialPosition; callback?.Invoke(); });
    }

    [Button, TabGroup("Zoom")]
    public void ZoomOutZoomInElastic(Action callback)
    {
        Vector3 initialPosition = transform.localPosition;
        DOTween.Sequence()
           .Append(transform.DOLocalMove(transform.localPosition - transform.forward, .5f))
           .Append(transform.DOLocalMove(transform.localPosition, .5f).SetEase(Ease.OutElastic))
           .OnComplete(() => { transform.localPosition = initialPosition; callback?.Invoke(); });
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