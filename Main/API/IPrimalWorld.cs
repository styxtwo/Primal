
using System;
using System.Collections.Generic;
namespace Primal.Api {

    /// <summary>
    /// The world in which all entities and systems exist, Create using the WorldFactory.
    /// </summary>
    public interface IPrimalWorld {

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
        /// <param name="system">The system(s) to add. </param>
        IPrimalWorld AddSystem(AbstractSystem systems);

        /// <summary>
        /// Adds several systems to the world.
        /// </summary>
        /// <param name="system">The systems to add. </param>
        IPrimalWorld AddSystems(params AbstractSystem[] systems);

        /// <summary>
        /// Creates an entity in the ECP world.
        /// </summary>
        /// <returns>The created entity</returns>
        IEntity CreateEntity();
        
        /// <summary>
        /// Removes an entity from the world.
        /// </summary>
        /// <param name="entity">The entity to remove. </param>
        void RemoveEntity(IEntity entity);

        /// <summary>
        /// Updates the systems, which work on the entities.
        /// </summary>
        /// <param name="elapsedMs">The elapsed milliseconds since the last update. </param>
        void Update(double elapsedMs);

        /// <summary>
        /// Updates the systems working on drawing the entities.
        /// </summary>
        /// <param name="elapsedMs">The elapsed milliseconds since the last update. </param>
        void Draw(double elapsedMs);
    }
}
