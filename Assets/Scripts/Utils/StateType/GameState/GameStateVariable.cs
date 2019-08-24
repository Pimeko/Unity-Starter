using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/State/Game State")]
public class GameStateVariable : SimpleRegisterableScriptableObject<GameStateTypeVariable>
{
    [SerializeField]
    GameStateTypeVariable newState;
    
    [Button("Update State")]
    public void UpdateState()
    {
        Value = newState;       
    }
}
