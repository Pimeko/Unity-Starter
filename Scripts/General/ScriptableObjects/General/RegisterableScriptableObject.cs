using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RegisterableScriptableObject : ScriptableObject
{
	public delegate void OnChangeDelegate();
    public OnChangeDelegate OnChange;

    protected void TriggerChange()
    {
        if (OnChange != null)
            OnChange();
    }
}