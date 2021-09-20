using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SerializedBetterAction : MonoBehaviour
{
    [SerializeField]
    BetterEvent action;
    
    [Button]
    public void Do()
    {
        action.Invoke();
    }
}