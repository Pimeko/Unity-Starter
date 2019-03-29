using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2Controller : MonoBehaviour
{
    [SerializeField]
    PayloadedGameEventInt testInt;
    [SerializeField]
    PayloadedGameEventInt testInt2;
    [SerializeField]
    BasicGameEvent basicEvent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            testInt.Raise(1);
        if (Input.GetKeyDown(KeyCode.Z))
            testInt2.Raise(2);
        if (Input.GetKeyDown(KeyCode.B))
            basicEvent.Raise();
    }
}