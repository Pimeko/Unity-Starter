using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventCollider : UnityEvent<Collider> { }

[System.Serializable]
public class TriggerAction : InteractionAction<UnityEventCollider> {}

public class OnTriggerController : OnInteractionController<TriggerAction, UnityEventCollider>
{
    void OnTriggerEnter(Collider other)
    {
        if (actions == null)
            return;

        if (actions.ContainsKey(other.tag))
        {
            actions[other.tag].OnInteraction?.Invoke(other);
            if (printDebug)
                print("Trigger " + other.name);
        }
    }
}