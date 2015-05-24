using Primal.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;
namespace Primal {
    class EntityPool {
        public event Action<IEntity> EntityChanged;

        private IList<IEntity> entities;

        public EntityPool() {
            entities = new List<IEntity>();
        }

        public void Add(IEntity entity) {
            entities.Add(entity);
            entity.ComponentsChanged += EntityComponentsChanged;
        }

        void EntityComponentsChanged(IEntity entity) {
            EntityChanged.NullSafeInvoke(entity);
        }

        public bool Remove(IEntity entity) {
            bool success = entities.Remove(entity);
            return success;
        }
    }
}
