using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public interface IBasicGameEventListener
{
    void Invoke();
}

public class BasicGameEventListener: GameEventListener<BasicGameEvent>, IBasicGameEventListener
{
    [SerializeField]
    bool ordered;
    
    [HideIf("ordered"), SerializeField]
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
    
    [ShowIf("ordered"), SerializeField, ReorderableList]
    List<UnityEvent> orderedActions;
    List<UnityEvent> OrderedActions
    {
        get
        {
            if (orderedActions == null)
                orderedActions = new List<UnityEvent>();
            return orderedActions;
        }
    }
    
    void IBasicGameEventListener.Invoke()
    {
        if (ordered)
            StartCoroutine(InvokeAfterDelay(OrderedActions));
        else
            StartCoroutine(InvokeAfterDelay(Actions));
    }

    IEnumerator InvokeAfterDelay(UnityEvent actions)
	{
        yield return new WaitForSeconds(delayBeforeAction);
        actions.Invoke();
	}

    IEnumerator InvokeAfterDelay(List<UnityEvent> actions)
	{
		yield return new WaitForSeconds(delayBeforeAction);
        foreach (UnityEvent action in actions)
        {
            if (action != null)
                action.Invoke();
        }
	}

    public void AddCallback(UnityAction callback)
    {
        if (ordered)
        {
            UnityEvent e = new UnityEvent();
            e.AddListener(callback);
            OrderedActions.Add(e);
        }
        else
            Actions.AddListener(callback);
    }
    
    public void Print(string str) { print(str); }
}