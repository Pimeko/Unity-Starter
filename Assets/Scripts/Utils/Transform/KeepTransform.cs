using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class KeepTransform : MonoBehaviour
{
    [SerializeField]
    bool keepPosition;
    [SerializeField, ShowIf("keepPosition")]
    bool keepPositionX = true, keepPositionY = true, keepPositionZ = true;
    [SerializeField]
    bool localPosition, specificPositionValue;
    [SerializeField, ShowIf("specificPositionValue")]
    Vector3 specificPosition;

    [SerializeField]
    bool keepRotation;
    [SerializeField]
    bool localRotation, specificRotationValue;
    [SerializeField, ShowIf("specificRotationValue")]
    Vector3 specificRotation;
    
    [SerializeField]
    bool recomputeOnEnable = false;

    Vector3 initialPosition;
    Quaternion initialRotation;

    void OnEnable()
    {
        if (recomputeOnEnable)
            Compute();
    }

    void Start()
    {
        Compute();
    }

    void Compute()
    {
        if (keepPosition)
            initialPosition = localPosition ? transform.localPosition : transform.position;
        if (keepRotation)
            initialRotation = localRotation ? transform.localRotation : transform.rotation;
    }

    void LateUpdate()
    {
        if (keepPosition)
        {
            var t = localPosition ? transform.localPosition : transform.position;
            Vector3 position = new Vector3(
                keepPositionX ? initialPosition.x : t.x,
                keepPositionY ? initialPosition.y : t.y,
                keepPositionZ ? initialPosition.z : t.z
            );
            if (localPosition)
                transform.localPosition = specificPositionValue ? specificPosition : position;
            else
                transform.position = specificPositionValue ? specificPosition : position;
        }
        if (keepRotation)
        {
            if (localRotation)
                transform.localRotation = specificRotationValue ? Quaternion.Euler(specificRotation) : initialRotation;
            else
                transform.rotation = specificRotationValue ? Quaternion.Euler(specificRotation) : initialRotation;
        }
    }
}