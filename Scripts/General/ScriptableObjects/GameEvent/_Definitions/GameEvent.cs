using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : ScriptableObject
{
    [SerializeField]
    bool logOnRaise;

    protected string logMessage
    {
        get
        {
            return "[!] Game Event '" + name + "' ";
        }
    }

    protected void Log(object value = null)
    {
        if (logOnRaise)
            Debug.Log(logMessage + (value == null ? "" : "| value = " + value.ToString()));
    }
}
