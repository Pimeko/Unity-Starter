using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterableListVariable<T> : SimpleRegisterableScriptableObject<List<T>>
{
    protected override void OnInit()
    {
		Value = new List<T>(initialValue);
		base.OnInit();
    }
}