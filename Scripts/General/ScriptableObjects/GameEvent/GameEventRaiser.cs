using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventRaiser : MonoBehaviour 
{
	[SerializeField]
	GameEvent toRaise;

	public void Raise()
	{
		toRaise.Raise();
	}
}
