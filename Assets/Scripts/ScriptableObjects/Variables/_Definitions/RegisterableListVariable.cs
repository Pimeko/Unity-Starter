using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class RegisterableListVariable<T> : RegisterableScriptableObject
{
    [SerializeField, TabGroup("Basic")]
    public List<T> initialValue;
    public List<T> InitialValue { get { return initialValue; } set { initialValue = value; OnInit(); } }

    [SerializeField, TabGroup("Basic")]
    protected List<T> value;
    [SerializeField, TabGroup("Basic")]
    protected List<T> previousValue;
    public List<T> Value { get { return value; } set { previousValue = this.value; this.value = value; TriggerChange(); } }
	public List<T> PreviousValue { get { return previousValue; } }
    [SerializeField, TabGroup("Basic")]
    IntVariable index;
	public IntVariable Index { get { return index; } }

    public int Count
    {
        get
        {
            return Value.Count;
        }
    }

    public T GetCurrent(bool moduloCount = false)
    {
        if (index == null)
            throw new UnityException("Must precise an int variable index");
        return moduloCount ? value[index.Value % value.Count] : value[index.Value];
    }
    
    public virtual void Add(T item)
    {
        Value.Add(item);
        TriggerChange();
    }

    public virtual void Remove(T item)
    {
        Value.Remove(item);
        TriggerChange();
    }

    public virtual void RemoveAt(int index)
    {
        Value.RemoveAt(index);
        TriggerChange();
    }

    public virtual void Clear()
    {
        Value.Clear();
        TriggerChange();
    }

    [Button]
    public void Shuffle()
    {
        initialValue.Shuffle();
        Value = initialValue;
    }
	
    protected override void OnInit()
    {
        if (initialValue != null)
            Value = new List<T>(initialValue);
    }
}