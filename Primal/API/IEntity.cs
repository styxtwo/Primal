using System;
using System.Collections.Generic;

namespace Primal.API {

    /// <summary>
    /// Provides an entity for the systems to work on. Contains a list of components.
    /// </summary>
    public interface IEntity {
        /// <summary>
        /// The event that gets called when the components of this entity change.
        /// </summary>
        event Action<IEntity> ComponentsChanged;

        /// <summary>
        /// Adds a component to the entity. Only one of each type may be added.
        /// </summary>
        /// <param name="component">The component to add. </param>
        /// <returns>Whether the component was added successfully. </returns>
        bool Add(IComponent component);

        /// <summary>
        /// Removes a component from the entity.
        /// </summary>
        /// <typeparam name="T">The type of the component to add. </typeparam>
        /// <returns>Whether the component was removed successfully. </returns>
        bool Remove<T>();

        /// <summary>
        /// Returns the component of a specific type. Null if no component is available.
        /// </summary>
        /// <typeparam name="T">The type of the component to return. </typeparam>
        /// <returns>The component with the parameter type. </returns>
        T Get<T>();

        /// <summary>
        /// Checks if the entity contains a component with a specific type.
        /// </summary>
        /// <typeparam name="T">The specific type of the component to check. </typeparam>
        /// <returns>Whether the component is present. </returns>
        bool Contains<T>();
        
        /// <summary>
        /// Checks if the entity contains a component with a specific type.
        /// </summary>
        /// <param name="type">The specific type of the component to check. </param>
        /// <returns>Whether the component is present. </returns>
        bool Contains(Type type);

        /// <summary>
        /// Checks if the entity contains all the input types.
        /// </summary>
        /// <param name="types">The list of types of the components to check. </param>
        /// <returns>Whether the entity contains all types. </returns>
        bool ContainsAll(IEnumerable<Type> types);
    }
}
