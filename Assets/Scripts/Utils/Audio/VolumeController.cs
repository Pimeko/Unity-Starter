using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [SerializeField]
    BoolVariable soundActive;

    void Start()
    {
        soundActive.AddOnChangeCallback(UpdateVolume);
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        AudioListener.volume = soundActive.Value ? 1 : 0;
    }

    void OnDestroy()
    {
        soundActive.RemoveOnChangeCallback(UpdateVolume);    
    }
}