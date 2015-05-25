
namespace Primal.API {

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
        /// Adds an entity to the world.
        /// </summary>
        /// <param name="entity">The entity to add. </param>
        void AddEntity(Entity entity);
        
        /// <summary>
        /// Removes an entity from the world.
        /// </summary>
        /// <param name="entity">The entity to remove. </param>
        void RemoveEntity(Entity entity);

        /// <summary>
        /// Updates the world, which in turn updates the systems, which work on the entities.
        /// </summary>
        /// <param name="elapsedMs">The elapsed milliseconds since the last update. </param>
        void Update(double elapsedMs);
    }
}
