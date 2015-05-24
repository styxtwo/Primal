using Primal.API;
using System;
using System.Collections.Generic;

namespace Primal {
    class EntityFinder : IFinder {
        Entities entities;
        public EntityFinder(Entities entities) {
            this.entities = entities;
        }

        public IEnumerable<IEntity> Find(IEnumerable<Type> components) {
            IList<IEntity> selected = new List<IEntity>();
            foreach (IEntity entity in entities.EntityList) {
                if (entity.ContainsAll(components)) {
                    selected.Add(entity);
                }
            }
            return selected;
        }
    }
}
