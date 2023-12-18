using System;
using UnityEngine;

namespace _Scripts.Weapons.Components.ComponentData.AttackData
{
    [Serializable]
    public class AttackSprites
    {
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }
}