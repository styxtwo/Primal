using Primal.API;
using System;
using System.Collections.Generic;
using Utilities.Extensions;
namespace Primal {
    /// <summary>
    /// Provides a collection for the entities.
    /// </summary>
    class Entities {
        public event Action<IEntity> EntityChanged;
        public event Action<IEntity> EntityAdded;
        public event Action<IEntity> EntityRemoved;

        private IList<IEntity> entities;

        public Entities() {
            entities = new List<IEntity>();
        }

        public void Add(IEntity entity) {
            entities.Add(entity);
            entity.ComponentsChanged += EntityComponentsChanged;
            EntityAdded.NullSafeInvoke(entity);
        }

        void EntityComponentsChanged(IEntity entity) {
            EntityChanged.NullSafeInvoke(entity);
        }

        public bool Remove(IEntity entity) {
            if (entities.Remove(entity)) {
                entity.ComponentsChanged -= EntityComponentsChanged;
                EntityRemoved.NullSafeInvoke(entity);
                return true;
            }
            return false;
        }

        public IEnumerable<IEntity> EntityList {
            get {
                return entities;
            }
        }
    }
}
