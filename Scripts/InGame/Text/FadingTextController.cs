using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadingTextController : MonoBehaviour
{
    [SerializeField]
    Vector3Variable lookAt;
    
    UnityEvent over;
    TextMesh textMesh;
    string content;

    void Awake()
    {
        textMesh = GetComponentInChildren<TextMesh>();
    }

    void Start()
    {
        FadingTextAnimationReceiver fadingTextAnimationReceiver = textMesh.GetComponent<FadingTextAnimationReceiver>();
        fadingTextAnimationReceiver.AddListenerAnimationOver(AnimationFadeOver);
    }

    void OnEnable()
    {
        textMesh.text = content;
        transform.LookAt(2 * transform.position - lookAt.Value);
    }

    public void Init(string content)
    {
        this.content = content;
    }

    public void AddListenerOver(UnityAction callback)
    {
        if (over == null)
            over = new UnityEvent();
        over.AddListener(callback);
    }

    public void AnimationFadeOver()
    {
        if (over != null)
            over.Invoke();
        gameObject.SetActive(false);
    }
}
