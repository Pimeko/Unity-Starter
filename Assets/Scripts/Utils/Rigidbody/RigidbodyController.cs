using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RigidbodyController : MonoBehaviour
{
    public Action<Collision> onCollisionEnter, onCollisionStay, onCollisionExit;
    public Action<Collider> onTriggerEnter, onTriggerStay, onTriggerExit;

    Rigidbody rb;
    public Rigidbody CurrentRigidbody { get { return rb; } }

    List<Collider> colliders;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        
        if (colliders == null)
            colliders = new List<Collider>();
        else
            colliders.Clear();

        colliders.Add(GetComponentsInChildren<Collider>().ToList());
    }

    public void EnableAllColliders()
    {
        colliders.ForEach(collider => collider.enabled = true);
    }

    public void DisableAllColliders()
    {
        colliders.ForEach(collider => collider.enabled = false);
    }

    # region Collision
    void OnCollisionEnter(Collision other)
    {
        onCollisionEnter?.Invoke(other);
    }

    void OnCollisionStay(Collision other)
    {
        onCollisionStay?.Invoke(other);
    }

    void OnCollisionExit(Collision other)
    {
        onCollisionExit?.Invoke(other);
    }
    # endregion

    # region Trigger
    void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke(other);
    }
    
    void OnTriggerStay(Collider other)
    {
        onTriggerStay?.Invoke(other);
    }
    
    void OnTriggerExit(Collider other)
    {
        onTriggerExit?.Invoke(other);
    }
    # endregion
}