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

    [SerializeField]
    bool lerp = false;
    [SerializeField, ShowIf("lerp")]
    float deltaTimeLerp;
    

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

    void UpdateData(bool usePhysics)
    {
        if (followPosition && toFollow != null)
        {
            Vector3 targetPosition = toFollow.position + offset;
            if (freezePosX)
                targetPosition.x = ApplyValue(targetPosition.x, transform.position.x, usePhysics);
            if (freezePosY)
                targetPosition.y = ApplyValue(targetPosition.y, transform.position.y, usePhysics);
            if (freezePosZ)
                targetPosition.z = ApplyValue(targetPosition.z, transform.position.z, usePhysics);

            transform.position = targetPosition;
        }

        if (followRotation && toFollow != null)
        {
            Vector3 targetRotationEuler = toFollow.rotation.eulerAngles;
            if (freezeRotX)
                targetRotationEuler.x = ApplyValue(targetRotationEuler.x, transform.rotation.eulerAngles.x, usePhysics);
            if (freezeRotY)
                targetRotationEuler.y = ApplyValue(targetRotationEuler.y, transform.rotation.eulerAngles.y, usePhysics);
            if (freezeRotZ)
                targetRotationEuler.z = ApplyValue(targetRotationEuler.z, transform.rotation.eulerAngles.z, usePhysics);

            transform.rotation = Quaternion.Euler(targetRotationEuler);
        }
    }

    float ApplyValue(float from, float to, bool usePhysics)
    {
        if (lerp)
            return Mathf.Lerp(from, to, (usePhysics ? Time.deltaTime : Time.fixedDeltaTime) * deltaTimeLerp);
        return to;
    }

    void Update()
    {
        if (onFixedUpdate || !isFollowing)
            return;
        UpdateData(false);
    }

    void FixedUpdate()
    {
        if (!onFixedUpdate || !isFollowing)
            return;
        UpdateData(true);
    }
}