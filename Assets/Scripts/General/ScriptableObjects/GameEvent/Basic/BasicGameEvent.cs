using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/Basic")]
public class BasicGameEvent : GameEvent
{
	private List<BasicGameEventListener> eventListeners = new List<BasicGameEventListener>();

	public void Raise()
	{
		Log();
		for(int i = eventListeners.Count -1; i >= 0; i--)
			eventListeners[i].OnEventRaised();
	}

	public void RegisterListener(BasicGameEventListener listener)
	{
		if (!eventListeners.Contains(listener))
			eventListeners.Add(listener);
	}

	public void UnregisterListener(BasicGameEventListener listener)
	{
		if (eventListeners.Contains(listener))
			eventListeners.Remove(listener);
	}
}
