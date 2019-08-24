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
    [SerializeField]
    bool triggerIfSameValue = false;
    
    public T Value
    {
        get
        {
            return value;
        }
        set
        {
            previousValue = this.value;
            this.value = value;

            bool equals = (previousValue != null && value != null) ? value.Equals(previousValue) : false;

            if (!equals || triggerIfSameValue)
                TriggerChange();
        }
    }
	public T PreviousValue { get { return previousValue; } }
	
    protected override void OnInit()
    {
        Value = initialValue;
    }
}