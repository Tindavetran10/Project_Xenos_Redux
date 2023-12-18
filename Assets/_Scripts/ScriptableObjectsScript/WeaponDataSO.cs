using System.Collections.Generic;
using System.Linq;
using _Scripts.Weapons.Components.ComponentData;
using UnityEngine;

namespace _Scripts.ScriptableObjectsScript
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon")]
    public class WeaponDataSO : ScriptableObject
    {
        [field: SerializeField] public int NumberOfAttacks { get; set; }
        protected float[] MovementSpeed { get; set; } 
        
        [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

        public T GetData<T>()
        {
            return ComponentData.OfType<T>().FirstOrDefault();
        }
        
        [ContextMenu("Add Sprite Data")]
        private void AddSpriteData() => ComponentData.Add(new WeaponSpriteData());
        
        [ContextMenu("Add Movement Data")]
        private void AddMovementData() => ComponentData.Add(new MovementData());
    }
}