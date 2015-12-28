using Primal.Api;
using System;
using System.Collections.Generic;
using Utilities.Extensions;

namespace Primal {

    /// <summary>
    /// Provides an entity for the systems to work on. Contains a list of components. 
    /// Contains several internal methods that are not nessesary to be used by users of the Primal framework.
    /// </summary>
    class Entity : IEntity {
        public event Action<Entity> ComponentsChanged;
        private IDictionary<Type, IComponent> components;

        public Entity() {
            components = new Dictionary<Type, IComponent>();
        }

        public IEntity Add(params IComponent[] components)
        {
            foreach(IComponent component in components) {
                AddComponent(component);
            }
            return this;
        }
            
        private Boolean AddComponent(IComponent component)
        {
            if (components.ContainsKey(component.GetType())) {
                return false;
            }
            components.Add(component.GetType(), component);
            ComponentsChanged.NullSafeInvoke(this);
            return true;
        }

        public IEntity Remove<T>() {
            if (components.Remove(typeof(T))) {
                ComponentsChanged.NullSafeInvoke(this);
            }
            return this;
        }

        public T Get<T>() where T : IComponent {
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

        public bool ContainsAll(IEnumerable<Type> types)
        {
            foreach (Type type in types) {
                if (!Contains(type)) {
                    return false;
                }
            }
            return true;
        }

        public int ComponentCount
        {
            get {
                return components.Count;
            }
        }

        public void Dispose()
        {
            components.Clear();
            ComponentsChanged = null;
        }
    }
}
