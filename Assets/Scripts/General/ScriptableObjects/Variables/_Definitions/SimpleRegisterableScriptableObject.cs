using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SimpleRegisterableScriptableObject<T> : RegisterableScriptableObject
{
    [SerializeField]
    protected T initialValue;

    [SerializeField]
    protected T value;
    [SerializeField, ReadOnly]
    protected T previousValue;
    public T Value { get { return value; } set { previousValue = this.value; this.value = value; TriggerChange(); } }
	public T PreviousValue { get { return previousValue; } }
	
    protected override void OnInit()
    {
        Value = initialValue;
    }
}