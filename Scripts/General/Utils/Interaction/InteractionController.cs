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

public class UnityEventInteraction : UnityEvent<Interaction>
{
    Interaction interaction;
}

public class InteractionController : MonoBehaviour
{
    [SerializeField]
    bool isTrigger = true;

    UnityEventInteraction onTrigger;
    
    public void AddListenerOnTrigger(UnityAction<Interaction> callback)
    {
        if (onTrigger == null)
            onTrigger = new UnityEventInteraction();
        onTrigger.AddListener(callback);
    }
    
    public void InvokeOnTrigger(Interaction value)
    {
        if (onTrigger != null)
            onTrigger.Invoke(value);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isTrigger)
            InvokeOnTrigger(new Interaction(other.gameObject));
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isTrigger)
            InvokeOnTrigger(new Interaction(other.gameObject));
    }
}