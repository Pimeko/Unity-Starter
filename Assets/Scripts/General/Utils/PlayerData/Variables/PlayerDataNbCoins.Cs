using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataNbCoins : MonoBehaviour, IPlayerData
{
    [SerializeField]
    IntVariable nbCoins;

    public PlayerData Save(PlayerData playerData)
    {
        playerData.nbCoins = nbCoins.Value;
        return playerData;
    }

    public void Load(PlayerData playerData)
    {
        nbCoins.Value = playerData.nbCoins;
    }
}