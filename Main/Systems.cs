using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities.Extensions;

namespace Primal {
    /// <summary>
    /// Provides a collection for the systems.
    /// </summary>
    class Systems {
        private IDictionary<Type, SystemWrapper> updateSystems;
        private IDictionary<Type, SystemWrapper> drawSystems;

        public Systems(Entities entities) {
            updateSystems = new Dictionary<Type, SystemWrapper>();
            drawSystems = new Dictionary<Type, SystemWrapper>();
            entities.EntityAdded += EntityAdded;
            entities.EntityRemoved += EntityRemoved;
            entities.EntityChanged += EntityChanged;
        }

        //-- Addition methods --//
        public bool Add(BaseSystem system, IEnumerable<IEntity> existingEntities) {
            return Add(system, existingEntities, updateSystems);
        }

        public bool Add(DrawSystem system, IEnumerable<IEntity> existingEntities) {
            return Add(system, existingEntities, drawSystems);
        }

        public bool Add(AbstractSystem system, IEnumerable<IEntity> existingEntities, IDictionary<Type, SystemWrapper> container) {
            if (container.ContainsKey(system.GetType())) {
                return false;
            }
            SystemWrapper wrapper = new SystemWrapper(system);
            container.Add(system.GetType(), wrapper);

            foreach (Entity enitity in existingEntities) {
                wrapper.AddEntity(enitity);
            }
            return true;
        }

        //-- Update methods --//
        internal void Update(double elapsedMs) {
            Update(elapsedMs, updateSystems.Values);
        }

        internal void Draw(double elapsedMs, params Type[] excluded) {
            Update(elapsedMs, drawSystems.Values);
        }
        
        private void Update(double elapsedMs, IEnumerable<SystemWrapper> systems) {
            foreach (SystemWrapper system in systems) {
                system.Update(elapsedMs);
            }
        }

        //-- Entity Added/Removed Methods --//
        private void EntityAdded(Entity entity) {
            foreach (SystemWrapper system in updateSystems.Values) {
                system.AddEntity(entity);
            }
        }

        private void EntityRemoved(Entity entity) {
            foreach (SystemWrapper system in updateSystems.Values) {
                system.RemoveEntity(entity);
            }
        }

        private void EntityChanged(Entity entity) {
            foreach (SystemWrapper system in updateSystems.Values) {
                system.UpdateEntityValidity(entity);
            }
        }

        //-- Debug data Methods --// 
        public int GetEntityCount(AbstractSystem system) {
            SystemWrapper wrapper;
            updateSystems.TryGetValue(system.GetType(), out wrapper);
            if (wrapper == null) {
                throw (new ArgumentException("System not found."));
            }
            return wrapper.EntityCount;
        }

        public int BaseSystemCount {
            get {
                return updateSystems.Count;
            }
        }

        public int DrawSystemCount {
            get {
                return drawSystems.Count;
            }
        }

        public IEnumerable<Type> BaseSystems() {
            return updateSystems.Keys;
        }

        public IEnumerable<Type> DrawSystems() {
            return drawSystems.Keys;
        }
    }
}
