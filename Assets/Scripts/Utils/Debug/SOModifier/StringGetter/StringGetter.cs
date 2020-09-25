using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StringGetter : MonoBehaviour
{
    protected string value;
    public Action<string> onValueChange;

    public abstract void Set(string value);
}