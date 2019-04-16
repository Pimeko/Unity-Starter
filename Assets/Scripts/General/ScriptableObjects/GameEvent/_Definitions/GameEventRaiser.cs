using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventRaiser<T_GAME_EVENT> : MonoBehaviour
{
    [SerializeField]
    protected T_GAME_EVENT gameEvent;
}