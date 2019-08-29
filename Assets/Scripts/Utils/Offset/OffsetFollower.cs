using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetFollower : MonoBehaviour
{
    [SerializeField]
    Transform toFollow;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - toFollow.position;
    }
    
    void Update()
    {
        Vector3 targetPosition = toFollow.position + offset;
        targetPosition.y = transform.position.y;
        
        transform.position = targetPosition;
    }
}