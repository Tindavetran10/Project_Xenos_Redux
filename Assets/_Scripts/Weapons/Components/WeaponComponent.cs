using _Scripts.CoreSystem;
using UnityEngine;

namespace _Scripts.Weapons.Components
{
    // Cannot attach to any Game Object
    // Only able to attach if a class inherit the WeaponComponent
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon Weapon;

        //protected AnimationEventHandler EventHandler => Weapon.EventHandler;
        protected AnimationEventHandler EventHandler;
        
        protected Core Core => Weapon.Core;
        
        protected bool IsAttackActive;

        
        protected virtual void Awake()
        {
            Weapon = GetComponent<Weapon>();
            EventHandler = GetComponentInChildren<AnimationEventHandler>();
        }

        protected virtual void Start() {}

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

    public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2 : AttackData
    {
        protected T1 Data;
        protected T2 CurrentAttackData;

        protected override void HandleEnter()
        {
            base.HandleEnter();
            CurrentAttackData = Data.AttackData[Weapon.CurrentAttackCounter];
        }

        protected override void Awake()
        {
            base.Awake();
            Data = Weapon.Data.GetData<T1>();
        }
    }
}