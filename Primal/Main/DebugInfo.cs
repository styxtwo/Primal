using Primal.API;

namespace Primal{

    /// <summary>
    /// Provides an implementation of the IDebugInfo interface.
    /// </summary>
    class DebugInfo : IDebugInfo {
        private Entities entities;
        private Systems systems;

        public DebugInfo(Entities entities, Systems systems) {
            this.entities = entities;
            this.systems = systems;
        }

        public int TotalSystemCount {
            get {
                return systems.SystemCount;
            }
        }

        public int TotalEntityCount {
            get {
                return entities.EntityCount;
            }
        }

        public int TotalComponentCount {
            get {
                return entities.ComponentCount;
            }
        }

        public int ComponentCount(Entity entity) {
            return entity.ComponentCount;
        }

        public int EntityCount(BaseSystem system) {
            return systems.GetEntityCount(system);
        }
    }
}
