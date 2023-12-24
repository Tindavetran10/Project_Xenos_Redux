using System;
using UnityEngine;

namespace _Scripts.CoreSystem.StatsSystem
{
    [Serializable]
    public class Stat
    {
        public event Action OnCurrentValueZero;
        [field: SerializeField] public float MaxValue { get; private set; }

        public float CurrentValue
        {
            get => _currentValue;
            private set
            {
                _currentValue = Mathf.Clamp(value, 0f, MaxValue);
                if (_currentValue <= 0) OnCurrentValueZero?.Invoke();
            }
        }

        private float _currentValue;

        public void Init() => CurrentValue = MaxValue;

        public void Increase(float amount) => CurrentValue += amount;
        public void Decrease(float amount) => CurrentValue -= amount;
    }
}