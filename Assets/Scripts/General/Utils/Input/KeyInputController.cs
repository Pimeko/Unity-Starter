using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public enum KeyInputType
{
    KEY_DOWN,
    KEY_UP,
    KEY_BEING_PRESSED
}

[System.Serializable]
public class KeyEvent
{
    [SerializeField]
    KeyCode key;
    public KeyCode Key { get { return key; } }
    [SerializeField]
    KeyInputType type;
    public KeyInputType Type { get { return type; } }
    [SerializeField]
    UnityEvent actions;
    public UnityEvent Actions { get { return actions; } }
}

public class KeyInputController : MonoBehaviour
{
    [SerializeField, ReorderableList]
    List<KeyEvent> keyEvents;

    private void Update()
    {
        foreach (KeyEvent keyEvent in keyEvents)
        {
            switch (keyEvent.Type)
            {
                case KeyInputType.KEY_DOWN:
                    if (Input.GetKeyDown(keyEvent.Key))
                        keyEvent.Actions?.Invoke();
                    break;
                case KeyInputType.KEY_UP:
                    if (Input.GetKeyUp(keyEvent.Key))
                        keyEvent.Actions?.Invoke();
                    break;
                case KeyInputType.KEY_BEING_PRESSED:
                    if (Input.GetKey(keyEvent.Key))
                        keyEvent.Actions?.Invoke();
                    break;
            }
        }
    }
}