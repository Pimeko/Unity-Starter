using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField]
    CollisionDetectionMode onMode = CollisionDetectionMode.ContinuousDynamic;
    [SerializeField]
    CollisionDetectionMode offMode = CollisionDetectionMode.ContinuousSpeculative;
    [SerializeField]
    bool enableOnStart;
    
    Rigidbody[] rigidbodies;
    Rigidbody[] Rigidbodies => transform.CachedComponentsInChildren(ref rigidbodies);

    Collider[] colliders;
    Collider[] Colliders => transform.CachedComponentsInChildren(ref colliders);
    
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

        if (enableOnStart)
            EnableRagdoll();
        else
            DisableRagdoll(true);
    }

    [Button("Enable")]
    public void EnableRagdoll(bool forceEnableColliders = false)
    {
        CurrentAnimator.enabled = false;
        foreach (var rb in Rigidbodies)
        {
            rb.isKinematic = false;
            rb.collisionDetectionMode = onMode;
        }
        
        if (forceEnableColliders)
        {
            foreach (var collider in Colliders)
                collider.enabled = true;
        }
    }

    [Button("Disable")]
    public void DisableRagdoll(bool disableColliders = false)
    {
        CurrentAnimator.enabled = true;
        foreach (var rb in Rigidbodies)
        {
            rb.isKinematic = true;
            rb.collisionDetectionMode = offMode;
        }

        if (disableColliders)
        {
            foreach (var collider in Colliders)
                collider.enabled = false;
        }
    }

    public void AddForceToAll(Vector3 force, ForceMode mode = ForceMode.Impulse)
    {
        foreach (var rb in Rigidbodies)
            rb.AddForce(force, mode);
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