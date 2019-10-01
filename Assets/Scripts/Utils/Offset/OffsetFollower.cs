using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetFollower : MonoBehaviour
{
    [SerializeField]
    Transform toFollow;
    [SerializeField]
    bool freezeX, freezeY, freezeZ;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - toFollow.position;
    }
    
    void Update()
    {
        Vector3 targetPosition = toFollow.position + offset;
        if (freezeX)
            targetPosition.x = transform.position.x;
        if (freezeY)
            targetPosition.y = transform.position.y;
        if (freezeZ)
            targetPosition.z = transform.position.z;
        
        transform.position = targetPosition;
    }
}