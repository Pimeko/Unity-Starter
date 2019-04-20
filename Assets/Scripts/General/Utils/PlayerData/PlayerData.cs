using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class PlayerData
{
	public override string ToString()
	{
		return JsonUtility.ToJson(this);
	}
}