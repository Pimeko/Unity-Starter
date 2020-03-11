using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    Rigidbody[] rigidbodies;
    Rigidbody[] Rigidbodies => transform.CachedComponentsInChildren(ref rigidbodies);
    
    Animator animator;
    Animator CurrentAnimator => transform.CachedComponent(ref animator);
    
    List<RigidbodyController> rigidbodyControllers;
    Dictionary<int, int> collisionsIds, triggersIds;

    public Action<Collider> onAnyTriggerEnter, onAnyTriggerExit;
    public Action<Collision> onAnyCollisionEnter, onAnyCollisionExit;

    void Start()
    {
        rigidbodyControllers = GetComponentsInChildren<RigidbodyController>().ToList();

        collisionsIds = new Dictionary<int, int>();
        triggersIds = new Dictionary<int, int>();
        
        rigidbodyControllers.ForEach(rbController =>
        {
            rbController.onCollisionEnter += OnAnyCollisionEnter;
            rbController.onCollisionEnter += OnAnyCollisionExit;

            rbController.onTriggerEnter += OnAnyTriggerEnter;
            rbController.onTriggerExit += OnAnyTriggerExit;
        });
    }

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
    
    void OnAnyTriggerEnter(Collider other)
    {
        int id = other.gameObject.GetInstanceID();
        if (triggersIds.ContainsKey(id))
        {
            triggersIds[id]++;
            return;
        }
        triggersIds.Add(id, 1);

        onAnyTriggerEnter?.Invoke(other);
    }

    void OnAnyTriggerExit(Collider other)
    {
        int id = other.gameObject.GetInstanceID();

        if (triggersIds.ContainsKey(id))
        {
            triggersIds[id]--;
            if (triggersIds[id] == 0)
            {
                triggersIds.Remove(id);
                onAnyTriggerExit?.Invoke(other);
            }
        }
    }
    
    void OnAnyCollisionEnter(Collision other)
    {
        int id = other.gameObject.GetInstanceID();
        if (triggersIds.ContainsKey(id))
        {
            triggersIds[id]++;
            return;
        }
        triggersIds.Add(id, 1);

        onAnyCollisionEnter?.Invoke(other);
    }

    void OnAnyCollisionExit(Collision other)
    {
        int id = other.gameObject.GetInstanceID();

        if (triggersIds.ContainsKey(id))
        {
            triggersIds[id]--;
            if (triggersIds[id] == 0)
            {
                triggersIds.Remove(id);
                onAnyCollisionExit?.Invoke(other);
            }
        }
    }

    void OnDestroy()
    {
        rigidbodyControllers.ForEach(rbController =>
        {
            rbController.onCollisionEnter -= OnAnyCollisionEnter;
            rbController.onCollisionEnter -= OnAnyCollisionExit;

            rbController.onTriggerEnter -= OnAnyTriggerEnter;
            rbController.onTriggerExit -= OnAnyTriggerExit;
        });
    }
}