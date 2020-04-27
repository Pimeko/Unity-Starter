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
    bool keepRotation;
    
    Vector3 initialPosition;
    Quaternion initialRotation;

    void Start()
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
            transform.position = position;
        }
        if (keepRotation)
            transform.rotation = initialRotation; 
    }
}