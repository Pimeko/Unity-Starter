using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SmoothAudioSource : MonoBehaviour
{
    AudioSource audioSource;
    AudioSource CurrentAudioSource => transform.CachedComponent(ref audioSource);

    bool isPlaying;
    float volumeToReach;
    Tween currentTween;

    private void Start()
    {
        volumeToReach = CurrentAudioSource.volume;
        currentTween = null;
    }

    public void Play()
    {
        if (!isPlaying)
        {
            DOTweenUtils.KillTween(ref currentTween);
            currentTween = DOVirtual.Float(0, volumeToReach, .2f, newVolume => CurrentAudioSource.volume = newVolume);
            if (CurrentAudioSource.gameObject.activeInHierarchy)
                CurrentAudioSource.Play();
            isPlaying = true;
        }
    }

    public void Stop()
    {
        if (isPlaying)
        {
            DOTweenUtils.KillTween(ref currentTween);
            currentTween = DOVirtual.Float(volumeToReach, 0, .2f, newVolume => CurrentAudioSource.volume = newVolume)
            .OnComplete(() =>
            {
                if (CurrentAudioSource.gameObject.activeInHierarchy)
                    CurrentAudioSource.Stop();
            });
            isPlaying = false;
        }
    }
}