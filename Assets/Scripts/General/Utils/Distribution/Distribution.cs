using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

[System.Serializable]
public class DistributionItem<T>
{
    [SerializeField]
    float weight;
    public float Weight { get { return weight; } set { weight = value; } }

    public float CombinedWeight { get; set; }

    [Range(0f, 100f), SerializeField]
    float percentage;
    public float Percentage { get { return percentage; } set { if (value >= 0 && value <= 100) percentage = value; } }

    [SerializeField]
    T value;
    public T Value { get { return value; } set { value = this.value; } }

    public DistributionItem()
    {
        CombinedWeight = 0;
        percentage = 0;
    }
}

public abstract class Distribution<T, T_ITEM> : MonoBehaviour
    where T_ITEM : DistributionItem<T>, new()
{
    [ReorderableList]
    [SerializeField]
    List<T_ITEM> items;
    public List<T_ITEM> Items { get { return items; } }

    List<T_ITEM> previousItems;

    float combinedWeight;

    void UpdatePreviousItems()
    {
        if (previousItems == null)
            previousItems = new List<T_ITEM>();
        else
            previousItems.Clear();
        foreach (T_ITEM item in items)
            previousItems.Add(new T_ITEM());
    }

    void OnItemsChange()
    {
        // Adding the component
        if (items == null)
            return;
        
        if (previousItems == null)
            UpdatePreviousItems();

        if (items.Count > previousItems.Count)
        {
            if (items.Count == 1)
                items[0].Weight = 1;
            else
                items[items.Count - 1].Weight = 0;
        }

        ComputePercentages();
        UpdatePreviousItems();
    }
    
    void ComputePercentages()
    {
        combinedWeight = 0;

        foreach (T_ITEM item in items)
        {
            combinedWeight += item.Weight;
            item.CombinedWeight = combinedWeight;
        }

        foreach (T_ITEM item in items)
            item.Percentage = item.Weight * 100 / combinedWeight;
    }

    void OnValidate()
    {
        OnItemsChange();
    }

    public T Draw()
    {
        if (items.Count == 0)
            throw new UnityException("Can't draw an item from an empty distribution!");

        float random = Random.Range(0f, combinedWeight);
        for (int i = 0; i < items.Count; i++)
        {
            T_ITEM item = items[i];
            if (item.CombinedWeight <= random)
                return item.Value;
        }

        throw new UnityException("Error while drawing");
    }

    public void Add(string name = "")
    {
        items.Add(new T_ITEM());
        OnItemsChange(); 
    }

    public void Remove(int index)
    {
        if (items.Count - 1 < index || index < 0)
            return;
        items.RemoveAt(index);
        OnItemsChange();
    }
}