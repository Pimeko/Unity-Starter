using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDataVariable<T_SO> : MonoBehaviour, IPlayerData
{
    [SerializeField]
    protected T_SO variable;

    public abstract void Init(ref PlayerData playerData);
    public abstract void Save(ref PlayerData playerData);
    public abstract void Load(PlayerData playerData);
}