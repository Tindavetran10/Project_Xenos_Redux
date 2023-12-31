using System;
using UnityEngine;

namespace _Scripts.Weapons.Components
{
    // Hold the damage of each attack
    [Serializable] 
    public class AttackDamage : AttackData
    {
        [field: SerializeField] public float Amount { get; private set; }
    }
}