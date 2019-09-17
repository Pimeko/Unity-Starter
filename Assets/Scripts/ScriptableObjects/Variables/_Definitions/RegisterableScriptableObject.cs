using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class RegisterableScriptableObject : SerializedScriptableObject, IRegisterableScriptableObject
{
    Action OnChange;

	void OnEnable()
	{
        OnInit();
	}

    protected virtual void OnInit() { }

    protected void TriggerChange()
    {
        OnChange?.Invoke();
    }

    public void AddOnChangeCallback(Action callback)
    {
        OnChange += callback;
    }

    public void RemoveOnChangeCallback(Action callback)
    {
        OnChange -= callback;
    }
}