using System;
using UnityEngine;

namespace KnightsQuest.CoreSystem.StatsSystem
{
    [Serializable]
    public class Stat
    {
        public event Action OnCurrentValueZero;

        [field: SerializeField] public float MaxValue { get; private set; }

        public float CurrentValue
        {
            get => currentValue;
            private set
            {
                // Makes sure currentValue never goes above maximumn
                currentValue = Mathf.Clamp(value, 0f, MaxValue);

                // Trigger event when value reaches zero
                if (currentValue <= 0f)
                {
                    OnCurrentValueZero?.Invoke();
                }
            }
        }
        private float currentValue;

        // Set value to max
        public void Init() => CurrentValue = MaxValue;

        public void Increase(float amount) => CurrentValue += amount;

        public void Decrease(float amount) => CurrentValue -= amount;
    }
}
