using System;
using UnityEngine;

namespace _Scripts.Weapons.Components
{
    [Serializable]
    public class AttackActionHitBox : AttackData
    {
        public bool debug;
        [field: SerializeField] public Rect HitBox { get; private set; }
    }
}