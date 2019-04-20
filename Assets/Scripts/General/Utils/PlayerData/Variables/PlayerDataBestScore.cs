using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerData
{
    public int bestScore;
}

public class PlayerDataBestScore : PlayerDataVariable<IntVariable>
{
    public override void Init(ref PlayerData playerData)
    {
        playerData.bestScore = 0;
    }

    public override void Save(ref PlayerData playerData)
    {
        playerData.bestScore = variable.Value;
    }

    public override void Load(PlayerData playerData)
    {
        variable.Value = playerData.bestScore;
    }
}