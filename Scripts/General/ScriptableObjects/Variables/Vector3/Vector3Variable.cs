using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Vector3")]
public class Vector3Variable : RegisterableScriptableObject
{
	[SerializeField]
	Vector3 initialValue;

	[SerializeField]
	private Vector3 value;
	public Vector3 Value
	{
		get { return value; }
		set { this.value = value; TriggerChange(); }
	}

	void OnEnable()
	{
		Value = initialValue;
	}
}
