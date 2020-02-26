using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    Rigidbody[] rigidbodies;
    Rigidbody[] Rigidbodies => transform.CachedComponentsInChildren(ref rigidbodies);
    
    Animator animator;
    Animator CurrentAnimator => transform.CachedComponent(ref animator);

    [Button("Enable")]
    public void EnableRagdoll()
    {
        CurrentAnimator.enabled = false;
        foreach (var rb in Rigidbodies)
            rb.isKinematic = false;
    }

    [Button("Disable")]
    public void DisableRagdoll()
    {
        CurrentAnimator.enabled = true;
        foreach (var rb in Rigidbodies)
            rb.isKinematic = true;
    }
}