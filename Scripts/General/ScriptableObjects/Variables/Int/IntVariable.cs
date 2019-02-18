using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Int")]
public class IntVariable : RegisterableScriptableObject
{
	[SerializeField]
	int initialValue;

	[SerializeField]
	int value;
	public int Value { get { return value; } set { this.value = value; TriggerChange(); } }
	
	void OnEnable()
	{
		Value = initialValue;
	}
}
