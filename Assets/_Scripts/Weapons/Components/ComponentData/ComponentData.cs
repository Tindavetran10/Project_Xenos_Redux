using System;
using UnityEngine;

namespace _Scripts.Weapons.Components
{
    // Base class that all type of data gonna inherited
    [Serializable]
    public abstract class ComponentData
    {
        [SerializeField, HideInInspector] private string name;

        public Type ComponentDependency { get; set; }
        
        public ComponentData()
        {
            SetComponentName();
            SetComponentDependency();
        }

        public void SetComponentName() => name = GetType().Name;
        protected abstract void SetComponentDependency();
        
        public virtual void SetAttackDataNames(){}

        public virtual void InitializeAttackData(int numberOfAttacks){}
    }

    [Serializable]
    public abstract class ComponentData<T> : ComponentData where T : AttackData
    {
        [SerializeField] private T[] attackData;
        public T[] AttackData { get => attackData; private set => attackData = value;}

        public override void SetAttackDataNames()
        {
            base.SetAttackDataNames();
            
            for (int i = 0; i < AttackData.Length; i++) 
                AttackData[i].SetAttackName(i + 1);
        }

        public override void InitializeAttackData(int numberOfAttacks)
        {
            base.InitializeAttackData(numberOfAttacks);
            
            var oldLen = attackData?.Length ?? 0;
            if(oldLen == numberOfAttacks) 
                return;

            Array.Resize(ref attackData, numberOfAttacks);

            if (oldLen < numberOfAttacks)
            {
                for (var i = oldLen; i < attackData.Length; i++)
                {
                    var newObj = Activator.CreateInstance(typeof(T)) as T;
                    attackData[i] = newObj;
                }
            }
            
            SetAttackDataNames();
        }
    }
}