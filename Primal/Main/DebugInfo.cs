using Primal.API;

namespace Primal{
    class DebugInfo : IDebugInfo {
        private int systems;
        private int entities;

        public DebugInfo(Entities entities, Systems systems) {
            entities.EntityAdded += EntityAdded;
            entities.EntityRemoved += EntityRemoved;
            systems.SystemAdded += SystemAdded;
        }

        private void SystemAdded(ISystem system) {
            systems++;
        }

        private void EntityAdded(IEntity entity) {
            entities++;
        }

        private void EntityRemoved(IEntity entity) {
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
