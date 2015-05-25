using System;
using System.Collections.Generic;

namespace Primal.Api {

    /// <summary>
    /// Provides a class that searches the list of entities for entities with specific components.
    /// </summary>
    public interface IFinder {

        /// <summary>
        /// Finds the entities that contain the correct components.
        /// </summary>
        /// <param name="components">The components that the entities should contain. </param>
        /// <returns>The collection of entities. </returns>
        IEnumerable<Entity> Find(IEnumerable<Type> components);
    }
}
