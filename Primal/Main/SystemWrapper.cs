using Primal.Api;
using System.Collections.Generic;
using System.Linq;

namespace Primal {
    /// <summary>
    /// Provides a wrapper around the systems, calls the methods of the system in the correct order. 
    /// Hides system implementation from the API users.
    /// </summary>
    class SystemWrapper {
        public BaseSystem System { get; private set; }
        private IList<Entity> entities;
        public SystemWrapper(BaseSystem system) {
            System = system;
            entities = new List<Entity>();
        }

        public void Update(double elapsedMs) {
            System.BeforeUpdate(elapsedMs);
            foreach (Entity entity in entities.ToList()) {
                System.UpdateEntity(entity, elapsedMs);
            }
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

        public void CheckEntityValidity(Entity entity) {
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

        public int EntityCount {
            get {
                return entities.Count();
            }
        }
    }
}
