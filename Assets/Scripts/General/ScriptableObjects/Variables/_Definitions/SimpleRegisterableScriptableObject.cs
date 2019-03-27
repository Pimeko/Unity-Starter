using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRegisterableScriptableObject<T> : RegisterableScriptableObject
{
	[SerializeField]
	protected T initialValue;

	[SerializeField]
	protected T value;
	public T Value { get { return value; } set { this.value = value; TriggerChange(); } }
    
    protected override void OnInit()
    {
		Value = initialValue;
    }
}