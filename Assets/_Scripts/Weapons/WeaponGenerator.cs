using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.ScriptableObjectsScript;
using _Scripts.Weapons.Components;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private WeaponDataSo data;

        private List<WeaponComponent> _componentAlreadyOnWeapon = new List<WeaponComponent>();

        private List<WeaponComponent> _componentsAddedToWeapon = new List<WeaponComponent>();

        private List<Type> _componentDependencies = new List<Type>();


        private void Start() => GenerateWeapon(data);

        public void GenerateWeapon(WeaponDataSo data)
        {
            weapon.SetData(data);
            
            _componentAlreadyOnWeapon.Clear();
            _componentsAddedToWeapon.Clear();
            _componentDependencies.Clear();

            _componentAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();
            _componentDependencies = data.GetAllDependencies();

            foreach (var dependency in _componentDependencies)
            {
                if(_componentsAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency)) 
                    continue;

                var weaponComponent =
                    _componentAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

                if (weaponComponent == null) 
                    weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
                
                weaponComponent.Init();
                
                _componentsAddedToWeapon.Add(weaponComponent);
            }

            var componentsToRemove = _componentAlreadyOnWeapon.Except(_componentsAddedToWeapon);
            
            foreach (var weaponComponent in componentsToRemove) 
                Destroy(weaponComponent);
        }
    }
}