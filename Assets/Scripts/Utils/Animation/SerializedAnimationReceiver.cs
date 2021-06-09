using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedAnimationReceiverKey : SerializedDictionaryValue<string>
{
    public DelayedUnityEvent onAction;
}

[System.Serializable]
public class SerializedAnimationReceiver : SerializedDictionary<string, SerializedAnimationReceiverKey> {}