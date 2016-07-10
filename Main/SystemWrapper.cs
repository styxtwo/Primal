using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Primal {
    /// <summary>
    /// Provides a wrapper around the systems, calls the methods of the system in the correct order. 
    /// Hides system implementation from the API users.
    /// </summary>
    class SystemWrapper {
        public AbstractSystem System { get; private set; }
        private IList<Entity> entities;
        public SystemWrapper(AbstractSystem system) {
            System = system;
            entities = new List<Entity>();
        }

        public void Update(double elapsedMs) {
            System.BeforeUpdate(elapsedMs);
            System.Update(entities, elapsedMs);
            System.AfterUpdate(elapsedMs);
        }

        public void AddEntity(Entity entity) {
            if (!entity.ContainsAll(System.KeyComponents)) {
                //entity does not have the right components
                return;
            }
            if (entities.Contains(entity)) {
                //system already contains the entity.
                return;
            }
            entities.Add(entity);
            System.EntityAdded(entity);
        }

        public void RemoveEntity(Entity entity) {
            if (!entities.Contains(entity)) {
                //system does not contain the entity.
                return;
            }
            entities.Remove(entity);
            System.EntityRemoved(entity);
        }

        public void UpdateEntityValidity(Entity entity) {
            if (entities.Contains(entity)) {
                if (!entity.ContainsAll(System.KeyComponents)) {
                    RemoveEntity(entity);
                }
            }
            else {
                if (entity.ContainsAll(System.KeyComponents)) {
                    AddEntity(entity);
                }
            }
        }

        public Boolean IsDraw() {
            return System.IsDraw();
        }

        public int EntityCount {
            get {
                return entities.Count();
            }
        }
    }
}
