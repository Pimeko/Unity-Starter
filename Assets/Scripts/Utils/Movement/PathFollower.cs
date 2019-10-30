using System;
using System.Collections;
using System.Collections.Generic;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public enum PathFollowerState
{
    IDLE,
    MOVING,
    STOPPED,
}

public class PathFollower : MonoBehaviour
{
    [SerializeField]
    BGCurve curve;

    [SerializeField]
    float movementSpeed = 3;
    public float MovementSpeed { get { return movementSpeed; } }
    [SerializeField]
    float rotationSpeed = 4;
    public float RotationSpeed { get { return rotationSpeed; } }

    [SerializeField]
    bool idleAtEachPoint = false;
    public bool IdleAtEachPoint { get { return idleAtEachPoint; } }
    [SerializeField, ShowIf("idleAtEachPoint")]
    float idleTime = 1;
    public float IdleTime { get { return idleTime; } }

    public Action<PathFollowerState> onStateChange;
    [SerializeField, ReadOnly]
    PathFollowerState currentState;
    public PathFollowerState CurrentState
    {
        get
        {
            return currentState;
        }
        private set
        {
            currentState = value;
            onStateChange?.Invoke(currentState);
        }
    }

    BGCcMath curveMath;
    float distanceDone, distanceTotal, distanceToDo, pointToPointDistanceDone;
    int currentPointIndex;
    bool isFollowingPath, isStopped, pointIndexGoingUp;
    Tween currentIdleTween;
    Rigidbody rb;

    void Awake()
    {
        curveMath = curve.GetComponent<BGCcMath>();
        rb = GetComponent<Rigidbody>();

        distanceDone = 0;
        distanceTotal = curveMath.GetDistance();

        currentPointIndex = 0;
        pointToPointDistanceDone = 0;
        distanceToDo = curveMath.GetDistance(currentPointIndex);
        pointIndexGoingUp = true;
        currentIdleTween = null;

        MovePosition(curveMath.CalcPositionByDistance(0));
        transform.forward = curve.Points[1].PositionWorld - curve.Points[0].PositionWorld;
    }

    [Button]
    public void Begin()
    {
        isFollowingPath = true;

        if (idleAtEachPoint)
        {
            CurrentState = PathFollowerState.MOVING;
            if (currentIdleTween != null)
                currentIdleTween.Play();
        }
        else
            CurrentState = PathFollowerState.MOVING;
    }

    [Button]
    public void Stop()
    {
        isFollowingPath = false;
        CurrentState = PathFollowerState.STOPPED;
        if (currentIdleTween != null)
            currentIdleTween.Pause();
    }

    void Update()
    {
        if (!isFollowingPath)
            return;

        if (idleAtEachPoint)
        {
            if (!isStopped)
            {
                Vector3 tangent;
                MovePosition(curveMath.CalcPositionAndTangentByDistance(distanceDone, out tangent));
                LerpRotation(TransformUtils.LookRotation(tangent));

                float currentDistanceDone = Time.deltaTime * movementSpeed;
                if (pointToPointDistanceDone + currentDistanceDone >= distanceToDo)
                {
                    float pointToPointDistanceDoneBefore = pointToPointDistanceDone;
                    pointToPointDistanceDone = distanceToDo;
                    distanceDone += pointToPointDistanceDone - pointToPointDistanceDoneBefore;
                }
                else
                {
                    distanceDone += currentDistanceDone;
                    pointToPointDistanceDone += currentDistanceDone;
                }
                if (pointToPointDistanceDone >= distanceToDo)
                {
                    isStopped = true;
                    CurrentState = PathFollowerState.IDLE;
                    UpdatePointIndexAndDistanceToDo();
                    currentIdleTween = DOVirtual.DelayedCall(idleTime, () =>
                    {
                        CurrentState = PathFollowerState.MOVING;
                        isStopped = false;
                        currentIdleTween = null;
                    });
                }
                distanceDone %= distanceTotal;
            }
        }
        else
        {
            Vector3 tangent;
            MovePosition(curveMath.CalcPositionAndTangentByDistance(distanceDone, out tangent));
            LerpRotation(TransformUtils.LookRotation(tangent));
            distanceDone += Time.deltaTime * movementSpeed;
            distanceDone %= distanceTotal;
        }
    }

    void MovePosition(Vector3 position)
    {
        if (rb != null)
            rb.position = position;
        else
            transform.position = position;
    }

    void LerpRotation(Quaternion to)
    {
        if (rb != null)
            rb.rotation = Quaternion.Lerp(rb.rotation, to, Time.deltaTime * rotationSpeed);
        else
            transform.rotation = Quaternion.Lerp(transform.rotation, to, Time.deltaTime * rotationSpeed);
    }

    void UpdatePointIndexAndDistanceToDo()
    {
        if (pointIndexGoingUp)
        {
            if (currentPointIndex + 1 == curve.Points.Length)
            {
                distanceToDo = TransformUtils.Distance(curveMath.GetDistance(currentPointIndex), curveMath.GetDistance(currentPointIndex - 1));
                currentPointIndex--;

                pointIndexGoingUp = false;
            }
            else
            {
                distanceToDo = TransformUtils.Distance(curveMath.GetDistance(currentPointIndex), curveMath.GetDistance(currentPointIndex + 1));
                currentPointIndex++;
            }
        }
        else
        {
            if (currentPointIndex == 0)
            {
                distanceToDo = TransformUtils.Distance(curveMath.GetDistance(currentPointIndex), curveMath.GetDistance(currentPointIndex + 1));
                currentPointIndex++;

                pointIndexGoingUp = true;
            }
            else
            {
                distanceToDo = TransformUtils.Distance(curveMath.GetDistance(currentPointIndex), curveMath.GetDistance(currentPointIndex - 1));
                currentPointIndex--;
            }
        }
        pointToPointDistanceDone = 0;
    }
}