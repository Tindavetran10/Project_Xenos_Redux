using System;
using UnityEngine;

namespace _Scripts.Weapons.Components
{
    // Base class that all type of data gonna inherited
    [Serializable]
    public class ComponentData {}

    [Serializable]
    public class ComponentData<T> : ComponentData where T : AttackData
    {
        [field: SerializeField] public T[] AttackData { get; private set; }
    }
}