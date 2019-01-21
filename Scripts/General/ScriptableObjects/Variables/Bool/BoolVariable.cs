using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Bool")]
public class BoolVariable : ScriptableObject
{
	[SerializeField]
	bool initialValue;

	[SerializeField]
	private bool value;
	public bool Value
	{
		get { return value; }
		set { this.value = value; }
	}

	void OnEnable()
	{
		Value = initialValue;
	}
}
