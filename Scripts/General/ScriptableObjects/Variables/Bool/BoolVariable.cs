using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Bool")]
public class BoolVariable : RegisterableScriptableObject
{
	[SerializeField]
	bool initialValue;

	[SerializeField]
	private bool value;
	public bool Value { get { return value; } set { this.value = value; TriggerChange(); } }

	void OnEnable()
	{
		Value = initialValue;
	}
}
