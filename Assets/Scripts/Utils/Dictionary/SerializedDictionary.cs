using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedDictionary<T_KEY, T_VALUE>
    where T_VALUE : SerializedDictionaryValue<T_KEY>
{
    [SerializeField]
    public List<T_VALUE> values;

    public T_VALUE this[T_KEY key]
    {
        get
        {
            foreach (T_VALUE value in values)
            {
                if (value.Key.Equals(key))
                    return value;
            }
            return null;
        }
    }
}

[System.Serializable]
public class SerializedDictionaryValue<T_KEY>
{
    [SerializeField]
    public T_KEY Key;
}