using Primal.Api;
using Primal.Main;
using System;
using System.Collections.Generic;
using Utilities.Extensions;
namespace Primal {
    /// <summary>
    /// Provides a collection for the entities.
    /// </summary>
    class Entities {
        public event Action<Entity> EntityChanged;
        public event Action<Entity> EntityAdded;
        public event Action<Entity> EntityRemoved;
        public EntityFinder EntityFinder { get; private set; }
        private ISet<Entity> entities;

        public Entities() {
            entities = new HashSet<Entity>();
            EntityFinder = new EntityFinder(entities);
        }

        public void Add(Entity entity) {
            if (entities.Add(entity)) {
                entity.ComponentsChanged += EntityComponentsChanged;
                EntityAdded.NullSafeInvoke(entity);
            }
        }

        void EntityComponentsChanged(Entity entity) {
            EntityChanged.NullSafeInvoke(entity);
        }

        public bool Remove(Entity entity) {
            if (entities.Remove(entity)) {
                entity.ComponentsChanged -= EntityComponentsChanged;
                EntityRemoved.NullSafeInvoke(entity);
                return true;
            }
            return false;
        }

        public int EntityCount {
            get {
                return entities.Count;
            }
        }

        public int ComponentCount {
            get {
                int count = 0;
                foreach (Entity entity in entities) {
                    count += entity.ComponentCount;
                }
                return count;
            }
        }
    }
}
