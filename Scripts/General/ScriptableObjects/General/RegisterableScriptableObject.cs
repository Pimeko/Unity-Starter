using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RegisterableScriptableObject<T> : ScriptableObject, IRegisterableScriptableObject
{
	delegate void OnChangeDelegate();
    OnChangeDelegate OnChange;

	[SerializeField]
	protected T initialValue;

	[SerializeField]
	protected T value;
	public T Value { get { return value; } set { this.value = value; TriggerChange(); } }
	
	void OnEnable()
	{
        OnInit();
		Value = initialValue;
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