using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGameEventRaiser : MonoBehaviour 
{
	[SerializeField]
	BasicGameEvent toRaise;

	public void Raise()
	{
		toRaise.Raise();
	}
}
