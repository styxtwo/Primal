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

        /// <summary>
        /// The event that gets called when the components of this entity change.
        /// </summary>
        public event Action<Entity> ComponentsChanged;

        /// <summary>
        /// The list of components.
        /// </summary>
        private IDictionary<Type, IComponent> components;

        /// <summary>
        /// Constructor of the entity.
        /// </summary>
        public Entity() {
            components = new Dictionary<Type, IComponent>();
        }

        /// <summary>
        /// Adds a component to the entity. Only one of each type may be added.
        /// </summary>
        /// <param name="component">The component to add. </param>
        /// <returns>Whether the component was added successfully. </returns>
        public bool Add(IComponent component)
        {
            if (components.ContainsKey(component.GetType())) {
                return false;
            }
            components.Add(component.GetType(), component);
            ComponentsChanged.NullSafeInvoke(this);
            return true;
        }

        /// <summary>
        /// Removes a component from the entity.
        /// </summary>
        /// <typeparam name="T">The type of the component to add. </typeparam>
        /// <returns>Whether the component was removed successfully. </returns>
        public bool Remove<T>() {
            if (components.Remove(typeof(T))) {
                ComponentsChanged.NullSafeInvoke(this);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the component of a specific type. Null if no component is available.
        /// </summary>
        /// <typeparam name="T">The type of the component to return. </typeparam>
        /// <returns>The component with the parameter type. </returns>
        public T Get<T>() where T : IComponent {
            IComponent component;
            components.TryGetValue(typeof(T), out component);
            return (T)component;
        }

        /// <summary>
        /// Checks if the entity contains a component with a specific type.
        /// </summary>
        /// <typeparam name="T">The specific type of the component to check. </typeparam>
        /// <returns>Whether the component is present. </returns>
        public bool Contains<T>() {
            return Contains(typeof(T));
        }

        /// <summary>
        /// Checks if the entity contains a component with a specific type.
        /// </summary>
        /// <param name="type">The specific type of the component to check. </param>
        /// <returns>Whether the component is present. </returns>
        public bool Contains(Type type) {
            return components.ContainsKey(type);
        }

        /// <summary>
        /// Checks if the entity contains all the input types.
        /// </summary>
        /// <param name="types">The list of types of the components to check. </param>
        /// <returns>Whether the entity contains all types. </returns>
        public bool ContainsAll(IEnumerable<Type> types)
        {
            foreach (Type type in types) {
                if (!Contains(type)) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns the count of the components.
        /// </summary>
        public int ComponentCount
        {
            get {
                return components.Count;
            }
        }

        /// <summary>
        /// Disposes of the Entity.
        /// </summary>
        public void Dispose()
        {
            components.Clear();
            ComponentsChanged = null;
        }
    }
}
