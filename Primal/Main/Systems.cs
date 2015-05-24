using Primal.API;
using System;
using System.Collections.Generic;
using Utilities.Extensions;

namespace Primal {
    /// <summary>
    /// Provides a collection for the systems.
    /// </summary>
    class Systems {
        public event Action<ISystem> SystemAdded;
        private IList<SystemWrapper> systems;

        public Systems(Entities entities) {
            systems = new List<SystemWrapper>();
            entities.EntityAdded += EntityAdded;
            entities.EntityRemoved += EntityRemoved;
            entities.EntityChanged += EntityChanged;
        }

        public void Add(ISystem system) {
            systems.Add(new SystemWrapper(system));
            SystemAdded.NullSafeInvoke(system);
        }

        internal void Update(double elapsedMs) {
            foreach (SystemWrapper system in systems) {
                system.Update(elapsedMs);
            }
        }

        private void EntityAdded(IEntity entity) {
            foreach (SystemWrapper system in systems) {
                system.AddEntity(entity);
            }
        }

        private void EntityRemoved(IEntity entity) {
            foreach (SystemWrapper system in systems) {
                system.RemoveEntity(entity);
            }
        }

        private void EntityChanged(IEntity entity) {
            foreach (SystemWrapper system in systems) {
                system.CheckEntityValidity(entity);
            }
        }
    }
}
