using System;
using System.Collections.Generic;

namespace Primal.API {
    /// <summary>
    /// Provides a class that searches the list of entities for entities with specific components.
    /// </summary>
    public interface IFinder {
        IEnumerable<IEntity> Find(IEnumerable<Type> components);
    }
}
