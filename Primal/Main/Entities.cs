using Primal.Api;
using System;
using System.Collections.Generic;
using Utilities.Extensions;
namespace Primal {
    /// <summary>
    /// Provides a collection for the entities.
    /// </summary>
    class Entities : IFinder {
        public event Action<Entity> EntityChanged;
        public event Action<Entity> EntityAdded;
        public event Action<Entity> EntityRemoved;

        private ISet<Entity> entities;

        public Entities() {
            entities = new HashSet<Entity>();
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

        public IEnumerable<Entity> Find(IEnumerable<Type> components) {
            IList<Entity> selected = new List<Entity>();
            foreach (Entity entity in entities) {
                if (entity.ContainsAll(components)) {
                    selected.Add(entity);
                }
            }
            return selected;
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
