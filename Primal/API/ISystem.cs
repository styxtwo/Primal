using Primal.API;
using System;
using System.Collections.Generic;

namespace Primal {
    /// <summary>
    /// Provides an interface for a system.
    /// Systems (only) act on entities with the correct components.
    /// 
    /// </summary>
    public interface ISystem {
        /// <summary>
        /// The components an entity needs for the system to act on it.
        /// </summary>
        IEnumerable<Type> KeyComponents { get; }

        /// <summary>
        /// An update for a specific entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="elapsedMs">he elapsed time since the last update.</param>
        void UpdateEntity(IEntity entity, double elapsedMs);

        /// <summary>
        /// Before a specific update.
        /// </summary>
        /// <param name="elapsedMs">The elapsed time since the last update.</param>
        void BeforeUpdate(double elapsedMs);

        /// <summary>
        /// After a specific update.
        /// </summary>
        /// <param name="elapsedMs">The elapsed time since the last update.</param>
        void AfterUpdate(double elapsedMs);

        /// <summary>
        /// Methods that gets called when the system adds an entity.
        /// </summary>
        /// <param name="entity"> The entity that got added. </param>
        void EntityAdded(IEntity entity);

        /// <summary>
        /// Method that gets called when the system removes an entity.
        /// </summary>
        /// <param name="entity">The entity that got removed.</param>
        void EntityRemoved(IEntity entity);
    }
}
