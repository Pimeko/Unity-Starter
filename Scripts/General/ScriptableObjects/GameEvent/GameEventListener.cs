using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
	[SerializeField]
	GameEvent gameEvent;
	[SerializeField]
	UnityEvent response;
	[SerializeField]
	float delayBeforeAction = 0f;

	private void OnEnable()
	{
		if (gameEvent != null)
			Register();
	}

	private void OnDisable()
	{
		if (gameEvent != null)
			Unregister();
	}

	public void Register()
	{
		gameEvent.RegisterListener(this);
	}

	public void Unregister()
	{
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
