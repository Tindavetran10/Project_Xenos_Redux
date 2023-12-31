// Import necessary namespaces
using System;
using UnityEngine;

// Define a namespace for weapon components
namespace _Scripts.Weapons.Components
{
    // Base class for all types of data to inherit
    [Serializable]
    public abstract class ComponentData
    {
        // Serialized field to hide the name in the Unity Inspector
        [SerializeField, HideInInspector] private string name;

        // Property to get or set the component dependency type
        public Type ComponentDependency { get; set; }

        // Constructor for ComponentData
        protected ComponentData()
        {
            // Set the component name and dependency during construction
            SetComponentName();
            SetComponentDependency();
        }

        // Method to set the component name based on the type
        public void SetComponentName() => name = GetType().Name;

        // Abstract method to be implemented in derived classes for setting the component dependency
        protected abstract void SetComponentDependency();

        // Virtual method for setting attack data names, can be overridden in derived classes
        public virtual void SetAttackDataNames() { }

        // Virtual method for initializing attack data, can be overridden in derived classes
        public virtual void InitializeAttackData(int numberOfAttacks) { }
    }

    // Generic abstract class for component data with a type constraint on AttackData
    [Serializable]
    public abstract class ComponentData<T> : ComponentData where T : AttackData
    {
        // Serialized array of attack data
        [SerializeField] private T[] attackData;

        // Property to get or set the array of attack data
        public T[] AttackData { get => attackData; private set => attackData = value; }

        // Override method to set attack data names based on the index
        public override void SetAttackDataNames()
        {
            base.SetAttackDataNames();

            for (var i = 0; i < AttackData.Length; i++)
                AttackData[i].SetAttackName(i + 1);
        }

        // Override method to initialize attack data based on the number of attacks
        public override void InitializeAttackData(int numberOfAttacks)
        {
            base.InitializeAttackData(numberOfAttacks);

            var oldLen = attackData?.Length ?? 0;
            // If the length matches, no need to resize the array
            if (oldLen == numberOfAttacks)
                return;

            // Resize the array to match the specified number of attacks
            Array.Resize(ref attackData, numberOfAttacks);

            // If the new length is greater than the old length, create new AttackData objects
            if (oldLen < numberOfAttacks)
            {
                for (var i = oldLen; i < attackData.Length; i++)
                {
                    var newObj = Activator.CreateInstance(typeof(T)) as T;
                    attackData[i] = newObj;
                }
            }

            // Set attack data names after initialization
            SetAttackDataNames();
        }
    }
}
