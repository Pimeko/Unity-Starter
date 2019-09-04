using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PatternController : MonoBehaviour
{
    [SerializeField]
    List<Transform> nodes;
    [SerializeField]
    float durationBetweenNodes, idleTime, rotationDuration;
    [SerializeField]
    Ease easeTypeMovement = Ease.Linear, easeTypeRotation = Ease.Linear;

    public Action startMoving, startIdle, stopMoving;

    int currentIndex;
    Tween currentTween;

    void OnEnable()
    {
        transform.position = nodes[0].position;
        IncrementIndex();
        currentTween = null;
    }

    void IncrementIndex()
    {
        currentIndex++;
        if (currentIndex == nodes.Count)
            currentIndex = 0;
    }

    public void Move()
    {
        startMoving?.Invoke();
        Vector3 direction = Vector3.Normalize(nodes[currentIndex].position - transform.position);
        currentTween = transform.DOMove(nodes[currentIndex].position, durationBetweenNodes)
            .SetEase(easeTypeMovement)
            .OnComplete(() => { transform.position = nodes[currentIndex].position; Idle(); });
        transform.DOLookAt(nodes[currentIndex].position, rotationDuration).SetEase(easeTypeRotation);
    }

    public void Idle()
    {
        if (idleTime > 0)
        {
            startIdle?.Invoke();
            currentTween = DOVirtual.DelayedCall(idleTime, () => { IncrementIndex(); Move(); });
        }
        else
        {
            IncrementIndex();
            Move();
        }
    }

    public void Stop()
    {
        stopMoving?.Invoke();
        if (currentTween != null)
            currentTween.Kill();
    }
}