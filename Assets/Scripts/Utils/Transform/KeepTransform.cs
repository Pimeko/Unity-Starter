using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTransform : MonoBehaviour
{
    [SerializeField]
    bool keepPosition, keepRotation;
    
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
            transform.position = initialPosition;
        if (keepRotation)
            transform.rotation = initialRotation; 
    }
}