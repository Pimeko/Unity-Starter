using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class KeyEvents
{
    [SerializeField]
    BetterEvent onKeyDown;
    public BetterEvent OnKeyDown { get { return onKeyDown; } }
    [SerializeField]
    BetterEvent onKeyUp;
    public BetterEvent OnKeyUp { get { return onKeyUp; } }
    [SerializeField]
    BetterEvent onKeyPressed;
    public BetterEvent OnKeyPressed { get { return onKeyPressed; } }
}

public class KeyInputController : SerializedMonoBehaviour
{
    [SerializeField]
    Dictionary<KeyCode, KeyEvents> actions;

    #if UNITY_EDITOR
    private void Update()
    {
        foreach (KeyCode keyCode in actions.Keys)
        {
            if (Input.GetKeyDown(keyCode))
                actions[keyCode].OnKeyDown.Invoke();
            if (Input.GetKeyUp(keyCode))
                actions[keyCode].OnKeyUp.Invoke();
            if (Input.GetKey(keyCode))
                actions[keyCode].OnKeyPressed.Invoke();
        }
    }
    #endif
}