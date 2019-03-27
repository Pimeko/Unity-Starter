using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RegisterableScriptableObject : ScriptableObject, IRegisterableScriptableObject
{
	delegate void OnChangeDelegate();
    OnChangeDelegate OnChange;

	void OnEnable()
	{
        OnInit();
	}

    protected virtual void OnInit() { }

    protected void TriggerChange()
    {
        if (OnChange != null)
            OnChange();
    }

    public void AddOnChangeCallback(Action callback)
    {
        OnChange += () => { callback(); };
    }

    public void RemoveOnChangeCallback(Action callback)
    {
        OnChange -= () => { callback(); };
    }
}