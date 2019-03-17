using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationOverReceiver : MonoBehaviour
{
    public delegate void OverDelegate();
    public OverDelegate OnOver;
    
    public void InvokeOver()
    {
        OnOver();
    }
}