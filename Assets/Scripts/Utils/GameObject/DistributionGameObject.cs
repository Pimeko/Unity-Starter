using UnityEngine;

[System.Serializable]
public class DistributionItemGameObject : DistributionItem<GameObject> {}

public class DistributionGameObject : Distribution<GameObject, DistributionItemGameObject> {}