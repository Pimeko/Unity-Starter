using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStateCallbacks : StateCallbacks<GameStateTypeVariable> {}

public class GameStateTypeListener : StateTypeListener<GameStateVariable, GameStateTypeVariable, GameStateCallbacks> { }