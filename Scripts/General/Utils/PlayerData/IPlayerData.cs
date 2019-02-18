using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerData
{
	PlayerData Save(PlayerData playerData);
    void Load(PlayerData playerData);
}