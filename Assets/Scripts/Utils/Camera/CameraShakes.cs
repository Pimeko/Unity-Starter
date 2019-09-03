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
}