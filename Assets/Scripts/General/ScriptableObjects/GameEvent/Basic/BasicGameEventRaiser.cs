using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGameEventRaiser : GameEventRaiser<BasicGameEvent>
{
    public void Raise()
    {
        gameEvent.Raise();
    }
}