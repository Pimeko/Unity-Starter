using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDisableCallback : NativeCallback
{
    void OnDisable()
    {
        DoActions();
    }
}