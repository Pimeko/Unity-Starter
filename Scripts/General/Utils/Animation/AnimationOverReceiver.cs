using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationOverReceiver : MonoBehaviour
{
    UnityEvent over;
    
    public void AddListenerOver(UnityAction callback)
    {
        if (over == null)
            over = new UnityEvent();
        over.AddListener(callback);
    }
    
    public void InvokeOver()
    {
        if (over != null)
            over.Invoke();
    }
}