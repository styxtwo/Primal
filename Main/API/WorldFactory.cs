using Primal.Api;

namespace Primal.Api {

    /// <summary>
    /// Provides a factory for creating the World in which all objects exist.
    /// </summary>
    public class WorldFactory {

        /// <summary>
        /// Creates the world.
        /// </summary>
        /// <param name="systems">Optional systems that get added to the world.</param>
        /// <returns>The world that got created. </returns>
        public static IPrimalWorld Create(params AbstractSystem[] systems) {
            return new World().AddSystems(systems);
        }
    }
}
