using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
	static bool DEBUG = false;

	[SerializeField]
	List<GameEvent> gameEvents;
	[SerializeField]
	UnityEvent response;
	[SerializeField]
	float delayBeforeAction = 0f;

	private void OnEnable()
	{
		if (gameEvents != null && gameEvents.Count > 0)
			Register();
		else if (DEBUG)
			Debug.LogWarning("[GameEventListener] No event registered for '" + name + "'");
	}

	private void OnDisable()
	{
		if (gameEvents != null && gameEvents.Count > 0)
			Unregister();
	}

	public void Register()
	{
		foreach (GameEvent gameEvent in gameEvents)
			gameEvent.RegisterListener(this);
	}

	public void Register(GameEvent gameEvent)
	{
		if (gameEvents == null)
			gameEvents = new List<GameEvent>();
		gameEvents.Add(gameEvent);
		gameEvent.RegisterListener(this);
	}

	public void Unregister()
	{
		foreach (GameEvent gameEvent in gameEvents)
			gameEvent.UnregisterListener(this);
	}

	public void AddListenerResponse(UnityAction callback)
	{
		if (response == null)
			response = new UnityEvent();
		response.AddListener(callback);
	}

	public void OnEventRaised()
	{
		StartCoroutine(InvokeAfterDelay());
	}

	IEnumerator InvokeAfterDelay()
	{
		yield return new WaitForSeconds(delayBeforeAction);
		response.Invoke();
	}
}
