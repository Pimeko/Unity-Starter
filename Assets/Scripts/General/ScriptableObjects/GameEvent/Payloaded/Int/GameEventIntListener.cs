using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventIntListener : PayloadedGameEventListener<int, GameEventInt, UnityEventInt>
{
    [SerializeField]
    UnityEventInt actions;
    UnityEventInt Actions
    {
        get
        {
            if (actions == null)
                actions = new UnityEventInt();
            return actions;
        }
    }

    public override void Invoke(object value)
    {
        Invoke(Actions, value);
    }
    
    public override void AddCallback(UnityAction<int> callback)
    {
        Actions.AddListener(callback);
    }
}