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

    public static void Shuffle<T>(this List<T> list)
    {
        for (int t = 0; t < list.Count; t++)
        {
            T tmp = list[t];
            int r = Random.Range(t, list.Count);
            list[t] = list[r];
            list[r] = tmp;
        }
    }

    public static void ShuffleWithSeed<T>(this List<T> list, int seed = 0)
    {
        //UnityEngine.Random.InitState(_seed);
        System.Random rng = new System.Random(seed);
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            //int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    
    public static int FindIndex<T>(this List<T> list, T element)
    {
        return list.FindIndex(el => el.Equals(element));
    }

    public static T Last<T>(this List<T> list)
    {
        return list[list.Count - 1];
    }

    public static void Clear<T>(ref List<T> list)
    {
        if (list == null)
            list = new List<T>();
        else
            list.Clear();
    }
}