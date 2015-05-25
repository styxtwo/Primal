using Primal.Api;
using System;
using System.Collections.Generic;
using Utilities.Extensions;

namespace Primal {
    /// <summary>
    /// Provides a collection for the systems.
    /// </summary>
    class Systems {
        public event Action<BaseSystem> SystemAdded;
        private IDictionary<Type, SystemWrapper> systems;

        public Systems(Entities entities) {
            systems = new Dictionary<Type, SystemWrapper>();
            entities.EntityAdded += EntityAdded;
            entities.EntityRemoved += EntityRemoved;
            entities.EntityChanged += EntityChanged;
        }

        public bool Add(BaseSystem system) {
            if (systems.ContainsKey(system.GetType())) {
                return false;
            }
            systems.Add(system.GetType(), new SystemWrapper(system));
            SystemAdded.NullSafeInvoke(system);
            return true;
        }

        internal void Update(double elapsedMs) {
            foreach (SystemWrapper system in systems.Values) {
                system.Update(elapsedMs);
            }
        }

        private void EntityAdded(Entity entity) {
            foreach (SystemWrapper system in systems.Values) {
                system.AddEntity(entity);
            }
        }

        private void EntityRemoved(Entity entity) {
            foreach (SystemWrapper system in systems.Values) {
                system.RemoveEntity(entity);
            }
        }

        private void EntityChanged(Entity entity) {
            foreach (SystemWrapper system in systems.Values) {
                system.CheckEntityValidity(entity);
            }
        }

        public int GetEntityCount(BaseSystem system) {
            SystemWrapper wrapper;
            systems.TryGetValue(system.GetType(), out wrapper);
            if (wrapper == null) {
                throw (new ArgumentException("System not found."));
            }
            return wrapper.EntityCount;
        }

        public int SystemCount {
            get {
                return systems.Count;
            }
        }
    }
}
