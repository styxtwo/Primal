
using System;
using System.Collections.Generic;
namespace Primal.Api {

    /// <summary>
    /// The world in which all entities and systems exist, Create using the WorldFactory.
    /// </summary>
    public interface IWorld {

        /// <summary>
        /// The debug info for this IWorld;
        /// </summary>
        IDebugInfo DebugInfo { get; }

        /// <summary>
        /// The entity finder for this world instance.
        /// </summary>
        IFinder EntityFinder { get; }

        /// <summary>
        /// Adds a system to the world.
        /// </summary>
        /// <param name="system">The system to add. </param>
        void AddSystem(BaseSystem system);

        /// <summary>
        /// Creates an entity in the ECP world.
        /// </summary>
        /// <returns>The created entity</returns>
        Entity CreateEntity();
        
        /// <summary>
        /// Removes an entity from the world.
        /// </summary>
        /// <param name="entity">The entity to remove. </param>
        void RemoveEntity(Entity entity);

        /// <summary>
        /// Updates the world, which in turn updates the systems, which work on the entities.
        /// </summary>
        /// <param name="elapsedMs">The elapsed milliseconds since the last update. </param>
        /// <param name="excluded">the types of the systems that are not updated.</param>
        void UpdateAll(double elapsedMs, params Type[] excluded);

        /// <summary>
        /// Updates a specific system.
        /// </summary>
        /// <param name="elapsedMs">The elapsed milliseconds since the last update. </param>
        /// <typeparam name="T">The type to update.</typeparam>
        void Update<T>(double elapsedMs);
    }
}
