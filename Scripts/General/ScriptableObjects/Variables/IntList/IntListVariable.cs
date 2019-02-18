using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Int List")]
public class IntListVariable : RegisterableScriptableObject
{
	[SerializeField]
	List<int> initialValue;

	[SerializeField]
	private List<int> value;
	public List<int> Value
	{
		get { return value; }
		set { this.value = new List<int>(value); TriggerChange(); }
	}

	public void Add(int i)
	{
		value.Add(i);
		TriggerChange();
	}

	void OnEnable()
	{
		Value = new List<int>(initialValue);
	}
}
