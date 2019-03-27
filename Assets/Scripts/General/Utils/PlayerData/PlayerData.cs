using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
	public int bestScore;
	public int nbCoins;

	public PlayerData()
	{
		bestScore = 0;
		nbCoins = 0;
	}

	public override string ToString()
	{
		return JsonUtility.ToJson(this);
	}
}