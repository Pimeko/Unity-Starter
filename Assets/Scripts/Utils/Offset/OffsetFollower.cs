using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class OffsetFollower : MonoBehaviour
{
    [SerializeField]
    Transform toFollow;
    [SerializeField]
    bool followOnStart = true;
    [SerializeField]
    bool onFixedUpdate = false;

    [SerializeField]
    bool followPosition = true;
    [SerializeField, ShowIfGroup("followPosition"), LabelText("Freeze X")]
    bool freezePosX;
    [SerializeField, ShowIfGroup("followPosition"), LabelText("Freeze Y")]
    bool freezePosY;
    [SerializeField, ShowIfGroup("followPosition"), LabelText("Freeze Z")]
    bool freezePosZ;

    [SerializeField]
    bool followRotation;
    [SerializeField, ShowIfGroup("followRotation"), LabelText("Freeze X")]
    bool freezeRotX;
    [SerializeField, ShowIfGroup("followRotation"), LabelText("Freeze Y")]
    bool freezeRotY;
    [SerializeField, ShowIfGroup("followRotation"), LabelText("Freeze Z")]
    bool freezeRotZ;

    Vector3 offset;
    bool isFollowing;

    void Start()
    {
        if (followOnStart)
        {
            offset = transform.position - toFollow.position;
            isFollowing = true;
        }
    }

    public void SetFollow(Transform toFollow)
    {
        this.toFollow = toFollow;
        offset = toFollow == null ? Vector3.zero : transform.position - toFollow.position;
    }

    public void Continue()
    {
        isFollowing = true;
    }

    public void Stop()
    {
        isFollowing = false;
    }

    void UpdateData()
    {

        if (followPosition && toFollow != null)
        {
            Vector3 targetPosition = toFollow.position + offset;
            if (freezePosX)
                targetPosition.x = transform.position.x;
            if (freezePosY)
                targetPosition.y = transform.position.y;
            if (freezePosZ)
                targetPosition.z = transform.position.z;

            transform.position = targetPosition;
        }

        if (followRotation && toFollow != null)
        {
            Vector3 targetRotationEuler = toFollow.rotation.eulerAngles;
            if (freezeRotX)
                targetRotationEuler.x = transform.rotation.eulerAngles.x;
            if (freezeRotY)
                targetRotationEuler.y = transform.rotation.eulerAngles.y;
            if (freezeRotZ)
                targetRotationEuler.z = transform.rotation.eulerAngles.z;

            transform.rotation = Quaternion.Euler(targetRotationEuler);
        }
    }

    void Update()
    {
        if (onFixedUpdate || !isFollowing)
            return;
        UpdateData();
    }

    void FixedUpdate()
    {
        if (!onFixedUpdate || !isFollowing)
            return;
        UpdateData();
    }
}