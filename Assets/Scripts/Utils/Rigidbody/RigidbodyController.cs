using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RigidbodyController : MonoBehaviour
{
    List<Collider> colliders;

    void OnEnable()
    {
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
}