using Primal.Api;

namespace Primal.Api {

    /// <summary>
    /// Provides a factory for creating the World in which all objects exist.
    /// </summary>
    public class WorldFactory {

        /// <summary>
        /// Creates the world.
        /// </summary>
        /// <returns>The world that got created. </returns>
        public static IPrimalWorld Create() {
            return new World();
        }
    }
}
