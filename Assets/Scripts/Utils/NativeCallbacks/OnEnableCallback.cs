using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnableCallback : NativeCallback
{
    void OnEnable()
    {
        DoActions();
    }
}