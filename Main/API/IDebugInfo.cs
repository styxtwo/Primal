
namespace Primal.Api {

    /// <summary>
    /// Provides some debug info.
    /// </summary>
    public interface IDebugInfo {

        /// <summary>
        /// The total amount of systems.
        /// </summary>
        int TotalSystemCount { get; }

        /// <summary>
        /// The total amount of entities.
        /// </summary>
        int TotalEntityCount { get; }

        /// <summary>
        /// The total amount of Components.
        /// </summary>
        int TotalComponentCount { get; }

        /// <summary>
        /// The amount of components in a specific entity.
        /// </summary>
        /// <param name="entity">The entity to check.</param>
        /// <returns>The amount of components.</returns>
        int ComponentCount(Entity entity);

        /// <summary>
        /// The amount of entities in a specific system.
        /// </summary>
        /// <param name="system">The system to check.</param>
        /// <returns>The amount of entities.</returns>
        int EntityCount(BaseSystem system);
    }
}
