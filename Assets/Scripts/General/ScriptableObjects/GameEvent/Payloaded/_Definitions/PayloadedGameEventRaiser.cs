using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayloadedGameEventRaiser<T, T_GAME_EVENT> : GameEventRaiser<T_GAME_EVENT>
    where T_GAME_EVENT : PayloadedGameEvent<T>
{
    public void Raise(T value)
    {
        gameEvent.Raise(value);
    }
}