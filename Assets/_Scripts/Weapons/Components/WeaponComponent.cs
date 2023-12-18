using System;
using _Scripts.CoreSystem;
using UnityEngine;

namespace _Scripts.Weapons.Components
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon Weapon;

        //protected AnimationEventHandler EventHandler => _weapon.EventHandler;
        
        protected AnimationEventHandler EventHandler;
        protected Core Core => Weapon.Core;

        protected bool IsAttackActive;

        protected virtual void Awake()
        {
            Weapon = GetComponent<Weapon>();
            EventHandler = GetComponentInChildren<AnimationEventHandler>();
        }

        protected virtual void HandleEnter() => IsAttackActive = true;

        protected virtual void HandleExit() => IsAttackActive = false;

        protected virtual void OnEnable()
        {
            Weapon.OnEnter += HandleEnter;
            Weapon.OnExit += HandleExit;
        }

        protected virtual void OnDisable()
        {
            Weapon.OnEnter -= HandleEnter;
            Weapon.OnExit -= HandleExit;
        }
    }
}