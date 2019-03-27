using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/Int")]
public class GameEventInt : GameEvent
{
	private List<GameEventIntListener> eventListeners = new List<GameEventIntListener>();

	public void Raise(int value)
	{
		Log(value);
		for(int i = eventListeners.Count -1; i >= 0; i--)
			eventListeners[i].OnEventRaised(value);
	}

	public void RegisterListener(GameEventIntListener listener)
	{
		if (!eventListeners.Contains(listener))
			eventListeners.Add(listener);
	}

	public void UnregisterListener(GameEventIntListener listener)
	{
		if (eventListeners.Contains(listener))
			eventListeners.Remove(listener);
	}
}
