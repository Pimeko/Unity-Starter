using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializedRewardedController : RewardedController
{
    [SerializeField]
    DelayedUnityEvent onValid, onInvalid;
    
    public void Play(REWARDED_TYPE type)
    {
        playRewarded.Raise(new RewardedEvent(type, onValid.Invoke, onInvalid.Invoke));
    }
}