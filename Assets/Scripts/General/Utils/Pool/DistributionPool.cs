[System.Serializable]
public class DistributionItemPool : DistributionItem<ObjectPoolTypeVariable> {}

public class DistributionPool : Distribution<ObjectPoolTypeVariable, DistributionItemPool> {}