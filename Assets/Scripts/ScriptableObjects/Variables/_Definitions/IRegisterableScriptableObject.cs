using System;

// Useful to gather RegisterableScriptableObjects
public interface IRegisterableScriptableObject
{
    void AddOnChangeCallback(Action callback);
    void RemoveOnChangeCallback(Action callback);
}