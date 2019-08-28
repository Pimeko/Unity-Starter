using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShakes : MonoBehaviour
{
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    [Button]
    public void SmallShakeHorizontal()
    {
        SmallShakeHorizontal(null);
    }

    public void SmallShakeHorizontal(Action callback)
    {
        Vector3 initialPosition = transform.position;
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position + transform.right * 0.05f, .1f))
           .Append(transform.DOMove(transform.position - transform.right * 0.05f, .1f))
           .Append(transform.DOMove(transform.position, .1f))
           .OnComplete(() => { transform.position = initialPosition; callback?.Invoke(); });
    }

    [Button]
    public void SmallShakeVertical()
    {
        SmallShakeVertical(null);
    }

    public void SmallShakeVertical(Action callback)
    {
        Vector3 initialPosition = transform.position;
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position + transform.up * 0.05f, .1f))
           .Append(transform.DOMove(transform.position - transform.up * 0.05f, .1f))
           .OnComplete(() => { transform.position = initialPosition; callback?.Invoke(); });
    }

    [Button]
    public void ZoomOutZoomIn()
    {
        ZoomOutZoomIn(null);
    }

    public void ZoomOutZoomIn(Action callback)
    {
        Vector3 initialPosition = transform.position;
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position - transform.forward, .5f))
           .Append(transform.DOMove(transform.position, .5f).SetEase(Ease.OutElastic))
           .OnComplete(() => { transform.position = initialPosition; callback?.Invoke(); });
    }

    [Button]
    public void SmallZoomOutZoomIn()
    {
        SmallZoomOutZoomIn(null);
    }

    public void SmallZoomOutZoomIn(Action callback)
    {
        Vector3 initialPosition = transform.position;
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position - transform.forward / 2, .15f))
           .Append(transform.DOMove(transform.position, .15f))
           .OnComplete(() => { transform.position = initialPosition; callback?.Invoke(); });
    }
}