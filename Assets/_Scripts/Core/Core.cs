using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KnightsQuest.CoreSystem
{
    public class Core : MonoBehaviour
    {
        private readonly List<CoreComponent> CoreComponents = new List<CoreComponent>();

        private void Awake()
        {

        }

        public void LogicUpdate()
        {
            foreach (CoreComponent component in CoreComponents)
            {
                component.LogicUpdate();
            }
        }

        public void AddComponent(CoreComponent component)
        {
            if (!CoreComponents.Contains(component))
            {
                CoreComponents.Add(component);
            }
        }

        public T GetCoreComponent<T>() where T : CoreComponent
        {
            // Look for Core Component of a specific type
            var comp = CoreComponents.OfType<T>().FirstOrDefault();

            if (comp)
                return comp;

            // If comp is null, see if we can find the Component in children
            comp = GetComponentInChildren<T>();

            if (comp)
                return comp;

            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}.");
            return null;
        }

        public T GetCoreComponent<T>(ref T value) where T : CoreComponent
        {
            value = GetCoreComponent<T>();
            return value;
        }
    }
}
