using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Int")]
public class IntVariable : SimpleRegisterableScriptableObject<int>
{
    public void Add(int x)
    {
        Value += x;
    }
}
