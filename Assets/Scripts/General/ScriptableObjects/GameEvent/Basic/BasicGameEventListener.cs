using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IBasicGameEventListener
{
    void Invoke();
}

public class BasicGameEventListener: GameEventListener<BasicGameEvent>, IBasicGameEventListener
{
    [SerializeField]
    UnityEvent actions;
    UnityEvent Actions
    {
        get
        {
            if (actions == null)
                actions = new UnityEvent();
            return actions;
        }
    }
    
    void IBasicGameEventListener.Invoke()
    {
        StartCoroutine(InvokeAfterDelay());
    }

    IEnumerator InvokeAfterDelay()
	{
		yield return new WaitForSeconds(delayBeforeAction);
        Actions.Invoke();
	}

    public void AddCallback(UnityAction callback)
    {
        Actions.AddListener(callback);
    }

}