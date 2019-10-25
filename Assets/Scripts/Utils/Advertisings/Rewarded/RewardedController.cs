using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedEvent
{
    REWARDED_TYPE type;
    public REWARDED_TYPE Type { get { return type; } }

    Action onRewardedValid;
    public Action OnRewardedValid { get { return onRewardedValid; } }

    Action onRewardedInvalid;
    public Action OnRewardedInvalid { get { return onRewardedInvalid; } }

    public RewardedEvent(REWARDED_TYPE type, Action onRewardedValid, Action onRewardedInvalid)
    {
        this.type = type;
        this.onRewardedValid = onRewardedValid;
        this.onRewardedInvalid = onRewardedInvalid;
    }
}

public abstract class RewardedController : MonoBehaviour
{
    [SerializeField]
    protected GameEventRewardedEvent playRewarded;
}