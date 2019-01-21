using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveByLerp : MonoBehaviour
{
	UnityEvent done;
    Vector3 from, to;
    float duration;
    Timer timer;
    bool moveLocalPosition;

    void Start()
    {
        from = moveLocalPosition ? transform.localPosition : transform.position;
        timer = gameObject.AddComponent<Timer>();
        timer.Init(Over, duration);
	}
	
    public void Init(Vector3 to, UnityAction callback = null, float duration = 0.2f, bool moveLocalPosition = false)
    {
        if (callback != null)
        {
            done = new UnityEvent();
            done.AddListener(callback);
        }
        this.to = to;
        this.duration = duration;
        this.moveLocalPosition = moveLocalPosition;
    }

    public void MoveToAndOver()
    {
        Over();
    }

    void Over()
    {
        transform.position = to;
        if (done != null)
            done.Invoke();
        Destroy(this);
    }

	void Update()
    {
        float percentage = timer.TimeSpent / duration;
        if (moveLocalPosition)
            transform.localPosition = Vector3.Lerp(from, to, percentage);
        else
            transform.position = Vector3.Lerp(from, to, percentage);
    }
}
