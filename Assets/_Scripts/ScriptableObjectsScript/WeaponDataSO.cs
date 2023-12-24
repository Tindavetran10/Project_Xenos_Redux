using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Weapons.Components;
using UnityEngine;

namespace _Scripts.ScriptableObjectsScript
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon")]
    public class WeaponDataSo : ScriptableObject
    {
        [field: SerializeField] public int NumberOfAttacks { get; set; }
        [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

        public T GetData<T>() => ComponentData.OfType<T>().FirstOrDefault();

        public List<Type> GetAllDependencies() => ComponentData.Select(component => component.ComponentDependency).ToList();

        public void AddData(ComponentData data)
        {
            if(ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
                return;
            
            ComponentData.Add(data);
        }
    }
}