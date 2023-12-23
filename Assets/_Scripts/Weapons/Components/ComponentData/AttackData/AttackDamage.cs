using System;
using UnityEngine;

namespace _Scripts.Weapons.Components
{
    [Serializable] 
    public class AttackDamage : AttackData
    {
        [field: SerializeField] public float Amount { get; private set; }
        
        
    }
}