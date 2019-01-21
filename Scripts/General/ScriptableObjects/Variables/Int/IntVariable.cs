using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Int")]
public class IntVariable : ScriptableObject
{
	[SerializeField]
	int initialValue;

	[SerializeField]
	private int value;
	public int Value
	{
		get { return value; }
		set { this.value = value; }
	}

	void OnEnable()
	{
		Value = initialValue;
	}
}
