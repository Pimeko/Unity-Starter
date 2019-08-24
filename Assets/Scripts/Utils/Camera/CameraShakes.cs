using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShakes : MonoBehaviour
{
    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    [Button("Small shake Horizontal")]
    public void SmallShakeHorizontal()
    {
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position + transform.right * 0.05f, .1f))
           .Append(transform.DOMove(transform.position - transform.right * 0.05f, .1f))
           .Append(transform.DOMove(transform.position, .1f));
    }

    [Button("Small shake Vertical")]
    public void SmallShakeVertical()
    {
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position + transform.up * 0.05f, .1f))
           .Append(transform.DOMove(transform.position - transform.up * 0.05f, .1f))
           .Append(transform.DOMove(transform.position, .1f));
    }

    [Button("Zoom out Zoom in")]
    public void ZoomOutZoomIn()
    {
        DOTween.Sequence()
           .Append(transform.DOMove(transform.position - transform.forward, .5f))
           .Append(transform.DOMove(transform.position, .5f).SetEase(Ease.OutElastic));
    }
}