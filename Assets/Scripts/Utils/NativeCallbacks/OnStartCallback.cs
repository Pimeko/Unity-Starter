using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnStartCallback : NativeCallback
{
    void Start()
    {
        DoActions();
    }
}