using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerData
{
    public int nbCoins;
}

public class PlayerDataNbCoins : PlayerDataVariable<IntVariable>
{
    public override void Init(ref PlayerData playerData)
    {
        playerData.nbCoins = 0;
    }

    public override void Save(ref PlayerData playerData)
    {
        playerData.nbCoins = variable.Value;
    }

    public override void Load(PlayerData playerData)
    {
        variable.Value = playerData.nbCoins;
    }
}