using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventInt : UnityEvent<int>
{
    public int value;
}

public class GameEventIntListener : MonoBehaviour
{
	static bool DEBUG = false;

	[SerializeField]
	List<GameEventInt> gameEvents;
	[SerializeField]
	UnityEventInt response;
	[SerializeField]
	float delayBeforeAction = 0f;

	private void OnEnable()
	{
		if (gameEvents != null && gameEvents.Count > 0)
			Register();
		else if (DEBUG)
			Debug.LogWarning("[GameEventIntListener] No event registered for '" + name + "'");
	}

	private void OnDisable()
	{
		if (gameEvents != null && gameEvents.Count > 0)
			Unregister();
	}

	public void Register()
	{
		foreach (GameEventInt gameEvent in gameEvents)
			gameEvent.RegisterListener(this);
	}

	public void Register(GameEventInt gameEvent)
	{
		if (gameEvents == null)
			gameEvents = new List<GameEventInt>();
		gameEvents.Add(gameEvent);
		gameEvent.RegisterListener(this);
	}

	public void Unregister()
	{
		foreach (GameEventInt gameEvent in gameEvents)
			gameEvent.UnregisterListener(this);
	}

	public void AddListenerResponse(UnityAction<int> callback)
	{
		if (response == null)
			response = new UnityEventInt();
		response.AddListener(callback);
	}

	public void OnEventRaised(int value)
	{
		StartCoroutine(InvokeAfterDelay(value));
	}

	IEnumerator InvokeAfterDelay(int value)
	{
		yield return new WaitForSeconds(delayBeforeAction);
		response.Invoke(value);
	}
}
