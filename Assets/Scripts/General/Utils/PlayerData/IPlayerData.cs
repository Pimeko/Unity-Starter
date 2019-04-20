using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerData
{
    void Init(ref PlayerData playerData);
	void Save(ref PlayerData playerData);
    void Load(PlayerData playerData);
}