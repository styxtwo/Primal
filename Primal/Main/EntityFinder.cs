using Primal.API;
using System;
using System.Collections.Generic;

namespace Primal {

    /// <summary>
    /// Provides an implementation of the IFinder interface.
    /// </summary>
    class EntityFinder : IFinder {
        Entities entities;
        public EntityFinder(Entities entities) {
            this.entities = entities;
        }

        public IEnumerable<Entity> Find(IEnumerable<Type> components) {
            IList<Entity> selected = new List<Entity>();
            foreach (Entity entity in entities.EntityList) {
                if (entity.ContainsAll(components)) {
                    selected.Add(entity);
                }
            }
            return selected;
        }
    }
}
