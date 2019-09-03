using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RegisterableListVariable<T> : RegisterableScriptableObject
{
    [SerializeField, ReorderableList]
    protected List<T> initialValue;

    [SerializeField, ReorderableList]
    protected List<T> value;
    [SerializeField]
    protected List<T> previousValue;
    public List<T> Value { get { return value; } set { previousValue = this.value; this.value = value; TriggerChange(); } }
	public List<T> PreviousValue { get { return previousValue; } }
    [SerializeField]
    IntVariable index;

    public T GetCurrent(bool moduloCount = false)
    {
        if (index == null)
            throw new UnityException("Must precise an int variable index");
        return moduloCount ? value[index.Value % value.Count] : value[index.Value];
    }
    
    public void Add(T item)
    {
        Value.Add(item);
        TriggerChange();
    }

    public void Remove(T item)
    {
        Value.Remove(item);
        TriggerChange();
    }
	
    protected override void OnInit()
    {
        Value = new List<T>(initialValue);
    }
}