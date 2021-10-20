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
    bool kinematicOnStart, disableCollidersOnStart;

    Rigidbody[] rigidbodies;
    Rigidbody[] Rigidbodies => transform.CachedComponentsInChildren(ref rigidbodies);

    Collider[] colliders;
    Collider[] Colliders => transform.CachedComponentsInChildren(ref colliders);

    Animator animator;
    Animator CurrentAnimator => transform.CachedComponent(ref animator);

    public List<RigidbodyController> rigidbodyControllers;
    Dictionary<int, int> collisionsIds, triggersIds;

    public Rigidbody Root
    {
        get
        {
            return Rigidbodies[0];
        }
    }

    public Vector3 RootPosition
    {
        get
        {
            return Rigidbodies[0].position;
        }
    }
    public Quaternion RootRotation
    {
        get
        {
            return Rigidbodies[0].rotation;
        }
    }

    public Action<Collider> onAnyTriggerEnter, onAnyTriggerExit;
    public Action<Collision> onAnyCollisionEnter, onAnyCollisionExit;

    Vector3 initialRootLocalPosition;

    void Awake()
    {
        rigidbodyControllers = GetComponentsInChildren<RigidbodyController>().ToList();

        rigidbodyControllers?.ForEach(rbController =>
        {
            rbController.onCollisionEnter += OnAnyCollisionEnter;
            rbController.onCollisionEnter += OnAnyCollisionExit;

            rbController.onTriggerEnter += OnAnyTriggerEnter;
            rbController.onTriggerExit += OnAnyTriggerExit;
        });

        initialRootLocalPosition = Rigidbodies[0].transform.localPosition;
    }

    void OnEnable()
    {
        collisionsIds = new Dictionary<int, int>();
        triggersIds = new Dictionary<int, int>();

        if (kinematicOnStart)
            DisableRagdoll(disableCollidersOnStart);
        else
            EnableRagdoll(!disableCollidersOnStart);

        Rigidbodies[0].transform.localPosition = initialRootLocalPosition;
    }

    [Button("Enable")]
    public void EnableRagdoll(bool forceEnableColliders = false)
    {
        if (CurrentAnimator != null)
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
        foreach (var rb in Rigidbodies)
        {
            rb.collisionDetectionMode = offMode;
            rb.isKinematic = true;
        }

        if (disableColliders)
        {
            foreach (var collider in Colliders)
                collider.enabled = false;
        }
        if (CurrentAnimator != null)
            CurrentAnimator.enabled = true;
    }

    public void FreezeBones()
    {
        foreach (var rb in Rigidbodies)
        {
            var position = rb.transform.position;
            var rotation = rb.transform.rotation;
            rb.collisionDetectionMode = offMode;
            rb.isKinematic = true;
            rb.transform.position = position;
            rb.transform.rotation = rotation;
        }
    }

    public void ResetBones()
    {
        animator.Rebind();
        foreach (var rb in Rigidbodies)
        {
            var position = rb.transform.position;
            var rotation = rb.transform.rotation;
            rb.collisionDetectionMode = offMode;
            rb.isKinematic = true;
            rb.transform.position = position;
            rb.transform.rotation = rotation;
        }
    }

    public void ResetRigidbodies()
    {
        foreach (var rb in Rigidbodies)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
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
        rigidbodyControllers?.ForEach(rbController =>
        {
            rbController.onCollisionEnter -= OnAnyCollisionEnter;
            rbController.onCollisionEnter -= OnAnyCollisionExit;

            rbController.onTriggerEnter -= OnAnyTriggerEnter;
            rbController.onTriggerExit -= OnAnyTriggerExit;
        });
    }
}