using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListExtensions
{
    public static T GetRandomItem<T>(this T[] list)
    {
        return list[Random.Range(0, list.Length)];
    }

    public static T GetRandomItem<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static T GetAndRemoveRandomItem<T>(this List<T> list)
    {
        var index = Random.Range(0, list.Count);
        var result = list[index];
        list.RemoveAt(index);
        return result;
    }

    public static T GetRandomItem<T>(this List<T> list, out int index)
    {
        index = Random.Range(0, list.Count);
        return list[index];
    }
    
    public static List<T> GetRandomDistinctItems<T>(this List<T> list, int n)
    {
        if (n >= list.Count)
            throw new System.Exception("Asking for too many random items.");
        return list.Shuffle().Take(n).ToList();
    }

    public static bool AllTheSameValue<T>(this List<T> list)
    {
        return !list.Distinct().Skip(1).Any();
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

    public static void AddN<T>(this List<T> list, T element, int n)
    {
        for (int i = 0; i < n; i++)
            list.Add(element);
    }

    public static void AddIfNotInside<T>(this List<T> list, T element)
    {
        if (list.Contains(element))
            return;
        list.Add(element);
    }

    public static void ForEach<T>(this List<T> elements, System.Action<T, int> action)
    {
        for (int i = 0; i < elements.Count; i++)
            action?.Invoke(elements[i], i);
    }

    public static List<T> Shuffle<T>(this List<T> list)
    {
        for (int t = 0; t < list.Count; t++)
        {
            T tmp = list[t];
            int r = Random.Range(t, list.Count);
            list[t] = list[r];
            list[r] = tmp;
        }
        return list;
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
        if (list.Count == 0)
            throw new System.Exception("Trying to get last element of empty list.");
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