using System.Collections.Generic;
using System.Linq;
using _Scripts.Weapons.Components.ComponentData;
using UnityEngine;

namespace _Scripts.ScriptableObjectsScript
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon")]
    public class WeaponDataSo : ScriptableObject
    {
        [field: SerializeField] public int NumberOfAttacks { get; set; }
        [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

        public T GetData<T>() => ComponentData.OfType<T>().FirstOrDefault();
        
        [ContextMenu("Add Movement Data")]
        private void AddMovementData() => ComponentData.Add(new MovementData());

    }
}