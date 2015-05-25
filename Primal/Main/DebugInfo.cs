using Primal.API;

namespace Primal{

    /// <summary>
    /// Provides an implementation of the IDebugInfo interface.
    /// </summary>
    class DebugInfo : IDebugInfo {
        private int systems;
        private int entities;

        public DebugInfo(Entities entities, Systems systems) {
            entities.EntityAdded += EntityAdded;
            entities.EntityRemoved += EntityRemoved;
            systems.SystemAdded += SystemAdded;
        }

        private void SystemAdded(BaseSystem system) {
            systems++;
        }

        private void EntityAdded(Entity entity) {
            entities++;
        }

        private void EntityRemoved(Entity entity) {
            entities--;
        }

        public int TotalSystemCount {
            get {
                return systems;
            }
        }

        public int TotalEntityCount {
            get {
                return entities;
            }
        }
    }
}
