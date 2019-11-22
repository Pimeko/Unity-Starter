using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Bool")]
public class BoolVariable : SimpleRegisterableScriptableObject<bool>
{
    public void Invert()
    {
        Value = !Value;
    }
}
