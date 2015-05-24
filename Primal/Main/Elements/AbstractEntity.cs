using Primal.API;
using System;
using System.Collections.Generic;
using Utilities.Extensions;

namespace Primal {
    /// <summary>
    /// Provides an implementation for the IEntity interface. 
    /// </summary>
    public class AbstractEntity : IEntity {
        public event Action<IEntity> ComponentsChanged;
        private IDictionary<Type, IComponent> components;

        public AbstractEntity() {
            components = new Dictionary<Type, IComponent>();
        }

        public bool Add(IComponent component) {
            if (components.ContainsKey(component.GetType())) {
                return false;
            }
            components.Add(component.GetType(), component);
            ComponentsChanged.NullSafeInvoke(this);
            return true;
        }

        public bool Remove<T>() {
            if (components.Remove(typeof(T))) {
                ComponentsChanged.NullSafeInvoke(this);
                return true;
            }
            return false;
        }

        public T Get<T>(){
            IComponent component;
            components.TryGetValue(typeof(T), out component);
            return (T)component;
        }

        public bool Contains<T>() {
            return Contains(typeof(T));
        }

        public bool Contains(Type type) {
            return components.ContainsKey(type);
        }

        public bool ContainsAll(IEnumerable<Type> types) {
            foreach (Type type in types) {
                if (!Contains(type)) {
                    return false;
                }
            }
            return true;
        }

        public void Dispose() {
            foreach (IComponent component in components.Values) {
                component.Dispose();
            }
            components.Clear();
            ComponentsChanged = null;
        }
    }
}
