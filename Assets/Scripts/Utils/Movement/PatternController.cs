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
    Ease easeType;

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
            .SetEase(easeType)
            .OnComplete(() => { transform.position = nodes[currentIndex].position; Idle(); });
        transform.DOLookAt(nodes[currentIndex].position, rotationDuration);
    }

    public void Idle()
    {
        startIdle?.Invoke();
        currentTween = DOVirtual.DelayedCall(idleTime, () => { IncrementIndex(); Move(); });
    }

    public void Stop()
    {
        stopMoving?.Invoke();
        if (currentTween != null)
            currentTween.Kill();
    }
}