using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventCollision : UnityEvent<Collision> { }

[System.Serializable]
public class CollisionAction : InteractionAction<UnityEventCollision> {}

public class OnCollisionController : OnInteractionController<CollisionAction, UnityEventCollision>
{
    void OnCollisionEnter(Collision other)
    {
        if (actions == null)
            return;

        Collider collider = other.collider;
        if (actions.ContainsKey(collider.tag))
        {
            actions[collider.tag].OnInteraction?.Invoke(other);
            if (printDebug)
                print("Collision " + collider.name);
        }
    }
}