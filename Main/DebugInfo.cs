using Primal.Api;
using System;
using System.Collections.Generic;

namespace Primal {
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
                return DrawSystemCount + BaseSystemCount;
            }
        }

        public int BaseSystemCount {
            get {
                return systems.BaseSystemCount;
            }
        }

        public int DrawSystemCount {
            get {
                return systems.DrawSystemCount;
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

        public IEnumerable<Type> BaseSystems {
            get {
                return systems.BaseSystems();
            }
        }

        public IEnumerable<Type> DrawSystems {
            get {
                return systems.DrawSystems();
            }
        }

        public int ComponentCount(IEntity entity) {
            return entity.ComponentCount;
        }

        public int EntityCount(AbstractSystem system) {
            return systems.GetEntityCount(system);
        }
    }
}
