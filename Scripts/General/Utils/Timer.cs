using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	UnityEvent done;
    float timeSpent, duration;
    public float TimeSpent
    {
        get { return timeSpent; }
    }
    bool isRunning;

    void Start()
    {
        timeSpent = 0f;
        isRunning = true;
    }

    public void Init(UnityAction callback, float duration)
    {
        done = new UnityEvent();
        done.AddListener(callback);
        this.duration = duration;
    }

    public void InvokeDone()
    {
        if (done != null)
            done.Invoke();
    }

    public void StopWithoutInvoking()
    {
        Destroy(this);
    }
    
    public void Pause()
    {
        isRunning = false;
    }

    public void Resume()
    {
        isRunning = true;
    }
    
    void Update()
    {
        if (!isRunning)
            return;
            
        timeSpent += Time.deltaTime;
        if (timeSpent >= duration)
        {
            InvokeDone();
            Destroy(this);
        }
    }
}
