using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListExtensions
{
    public static T GetRandomItem<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static bool AllTheSameValue<T>(this List<T> list, System.Func<T, bool> predicate)
    {
        return list.Select(predicate).Distinct().ToList().Count > 1;
    }

    public static void Add<T>(this List<T> list, List<T> other)
    {
        foreach (T element in other)
            list.Add(element);
    }

    public static void ForEach<T>(this List<T> elements, System.Action<T, int> action)
    {
        for (int i = 0; i < elements.Count; i++)
            action?.Invoke(elements[i], i);
    }
}