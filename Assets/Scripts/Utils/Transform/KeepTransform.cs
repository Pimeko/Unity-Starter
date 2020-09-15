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
    bool specificPositionValue;
    [SerializeField, ShowIf("specificPositionValue")]
    Vector3 specificPosition;

    [SerializeField]
    bool keepRotation;
    [SerializeField]
    bool specificRotationValue;
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
            initialPosition = transform.position;
        if (keepRotation)
            initialRotation = transform.rotation;
    }

    void Update()
    {
        if (keepPosition)
        {
            Vector3 position = new Vector3(
                keepPositionX ? initialPosition.x : transform.position.x,
                keepPositionY ? initialPosition.y : transform.position.y,
                keepPositionZ ? initialPosition.z : transform.position.z
            );
            transform.position = specificPositionValue ? specificPosition : position;
        }
        if (keepRotation)
            transform.rotation = specificRotationValue ? Quaternion.Euler(specificRotation) : initialRotation;
    }
}