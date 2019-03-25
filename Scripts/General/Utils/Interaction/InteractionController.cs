using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction
{
    GameObject hitObject;
    public GameObject HitObject { get { return hitObject; } }
    Vector3? hitPoint;
    public Vector3? HitPoint { get { return hitPoint; } }

    public Interaction(GameObject hitObject)
    {
        this.hitObject = hitObject;
        this.hitPoint = null;
    }

    public Interaction(GameObject hitObject, Vector3 hitPoint)
    {
        this.hitObject = hitObject;
        this.hitPoint = hitPoint;
    }
}

public class InteractionController : MonoBehaviour
{
    [SerializeField]
    bool checkAutoInteractions = true;
    [SerializeField]
    bool isTrigger = true;

    public delegate void OnInteractionDelegate(Interaction interaction);
    public OnInteractionDelegate OnInteraction;

    public void TriggerInteraction()
    {
        if (OnInteraction != null)
            OnInteraction(null);
    }

    void OnDestroy()
    {
        if (OnInteraction != null)
        {
            foreach(OnInteractionDelegate currentDelegate in OnInteraction.GetInvocationList())
                OnInteraction -= currentDelegate;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (checkAutoInteractions && isTrigger && OnInteraction != null)
            OnInteraction(new Interaction(other.gameObject));
    }

    void OnCollisionEnter(Collision other)
    {
        if (checkAutoInteractions && !isTrigger && OnInteraction != null)
            OnInteraction(new Interaction(other.gameObject));
    }
}