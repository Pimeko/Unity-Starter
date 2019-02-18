using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataBestScore : MonoBehaviour, IPlayerData
{
    [SerializeField]
    IntVariable bestScore;

    public PlayerData Save(PlayerData playerData)
    {
        playerData.bestScore = bestScore.Value;
        return playerData;
    }

    public void Load(PlayerData playerData)
    {
        bestScore.Value = playerData.bestScore;
    }
}