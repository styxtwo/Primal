using Primal.API;
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

        private IList<Entity> entities;

        public Entities() {
            entities = new List<Entity>();
        }

        public void Add(Entity entity) {
            entities.Add(entity);
            entity.ComponentsChanged += EntityComponentsChanged;
            EntityAdded.NullSafeInvoke(entity);
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

        public IEnumerable<Entity> EntityList {
            get {
                return entities;
            }
        }
    }
}
