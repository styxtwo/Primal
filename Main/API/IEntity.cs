using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Api
{
    /// <summary>
    /// Provides the interface of an entity for the systems to work on. 
    /// </summary>
    public interface IEntity
    {
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
        T Get<T>() where T : IComponent;

        /// <summary>
        /// Checks if the entity contains a component with a specific type.
        /// </summary>
        /// <typeparam name="T">The specific type of the component to check. </typeparam>
        /// <returns>Whether the component is present. </returns>
        bool Contains<T>();
        
        /// <summary>
        /// Returns the count of the components.
        /// </summary>
        int ComponentCount { get; }
    }
}
