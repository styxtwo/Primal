
namespace Primal.API {
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
    }
}
