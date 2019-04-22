using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

[System.Serializable]
public class DistributionItem
{
    [SerializeField]
    string name;

    [Range(0, 100)]
    [SerializeField]
    float percentage = 0;
    public float Percentage { get { return percentage; } set { if (value >= 0 && value <= 100) percentage = value; } }

    public DistributionItem()
    {
        name = "";
        percentage = 0;
    }

    public DistributionItem(DistributionItem item)
    {
        name = item.name;
        percentage = item.percentage;
    }

    public bool IsEqualTo(DistributionItem other)
    {
        return other != null && percentage == other.percentage;
    }
}

public class Distribution : MonoBehaviour
{
    [ReorderableList]
    [SerializeField]
    List<DistributionItem> items;
    public List<DistributionItem> Items { get { return items; } }

    List<DistributionItem> previousItems;

    void UpdatePreviousItems()
    {
        if (previousItems == null)
            previousItems = new List<DistributionItem>();
        else
            previousItems.Clear();
        foreach (DistributionItem item in items)
            previousItems.Add(new DistributionItem(item));
    }

    void OnItemsChange()
    {
        // Adding the component
        if (items == null)
            return;
            
        if (previousItems == null)
            UpdatePreviousItems();

        // On Delete
        if (items.Count < previousItems.Count)
        {
            Normalize();
            UpdatePreviousItems();
            return;
        }
        // On Add
        else if (items.Count > previousItems.Count)
        {
            if (items.Count == 1)
                items[0].Percentage = 100;
            else
                items[items.Count - 1].Percentage = 0;
            UpdatePreviousItems();
            return;
        }

        int index = FindChangedIndex();
        // Nothing changed
        if (IsTheSameInAnyOrder() || index == -1)
        {
            UpdatePreviousItems();
            return;
        }

        if (items.Count == 1)
        {
            items[0].Percentage = 100;
            UpdatePreviousItems();
            return;
        }

        // On Change
        DiluteFrom(index);
        Normalize();
        UpdatePreviousItems();
    }

    
    void OnValidate()
    {
        OnItemsChange();
    }

    void DiluteFrom(int index)
    {
        int otherIndex = -1;

        for (int i = 0; i < items.Count; i++)
        {
            if (i == index)
                continue;
            
            DistributionItem item = items[i];
            if (item.Percentage > 0)
            {
                otherIndex = i;
                break;
            }
        }
        otherIndex = otherIndex == -1 ? index == 0 ? 1 : 0 : otherIndex;
        
        float previousPercentage = previousItems[index].Percentage;
        float newPercentage = items[index].Percentage;
        float difference = Mathf.Abs(previousPercentage - newPercentage);
        bool added = previousPercentage < newPercentage;
        items[otherIndex].Percentage += difference * (added ? -1 : 1);
    }

    void Normalize()
    {
        float sum = items.Sum((DistributionItem item) => item.Percentage);
        if (sum == 100)
        {
            foreach (DistributionItem item in items)
                item.Percentage = Mathf.Round(item.Percentage);
            return;
        }

        float toAdd = (100 - sum) / items.Count;
        foreach (DistributionItem item in items)
        {
            item.Percentage += toAdd;
            item.Percentage = Mathf.Round(item.Percentage);
        }
    }

    int FindChangedIndex()
    {
        int i = 0;

        foreach (DistributionItem item in items)
        {
            DistributionItem previousItem = previousItems[i];
            if (!item.IsEqualTo(previousItem))
                return i;
            i++;
        }
        
        return -1;
    }

    bool IsTheSameInAnyOrder()
    {
        return previousItems.Sum((DistributionItem item) => item.Percentage) == items.Sum((DistributionItem item) => item.Percentage);
    }

    public int Draw()
    {
        if (items.Count == 0)
            throw new UnityException("Can't draw an item from an empty distribution!");

        float random = Random.Range(0f, 100f);
        float currentProbability = 0;
        for (int i = 0; i < items.Count; i++)
        {
            DistributionItem item = items[i];
            currentProbability += item.Percentage;
            if (random <= currentProbability)
                return i;
        }
        return 0;
    }

    public void Add(string name = "")
    {
        items.Add(new DistributionItem());
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