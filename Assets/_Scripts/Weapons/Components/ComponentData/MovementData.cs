using _Scripts.Weapons.Components.ComponentData.AttackData;
using UnityEngine;

namespace _Scripts.Weapons.Components.ComponentData
{
    public class MovementData : ComponentData
    {
        [field: SerializeField] public AttackMovement[] AttackData { get; private set; }
        
    }
}