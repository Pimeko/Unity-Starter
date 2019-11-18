using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class JointDisabler : MonoBehaviour
{
    Joint joint;
    Joint CurrentJoint => transform.CachedComponent(ref joint);

    Rigidbody lastConnectedBody;

    void Start()
    {
        lastConnectedBody = null;
    }

    [Button("Enable"), HorizontalGroup("ButtonGroup")]
    public void EnableJoint()
    {
        CurrentJoint.connectedBody = lastConnectedBody;
    }

    [Button("Disable"), HorizontalGroup("ButtonGroup")]
    public void DisableJoint()
    {
        lastConnectedBody = CurrentJoint.connectedBody;
        CurrentJoint.connectedBody = null;
    }
}