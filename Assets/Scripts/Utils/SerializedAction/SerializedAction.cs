using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SerializedAction : MonoBehaviour
{
    [SerializeField]
    DelayedUnityEvent action;
    
    [Button]
    public void Do()
    {
        action.Invoke();
    }
}