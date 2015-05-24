using Primal.API;
using System.Collections.Generic;
using System.Linq;

namespace Primal {
    /// <summary>
    /// Provides a wrapper around the systems, calls the methods of the system in the correct order. 
    /// Hides system implementation from the API users.
    /// </summary>
    class SystemWrapper {
        private IList<IEntity> entities;
        private ISystem system;
        public SystemWrapper(ISystem system) {
            this.system = system;
            entities = new List<IEntity>();
        }

        public void Update(double elapsedMs) {
            system.BeforeUpdate(elapsedMs);
            foreach (IEntity entity in entities.ToList()) {
                system.UpdateEntity(entity, elapsedMs);
            }
            system.AfterUpdate(elapsedMs);
        }

        public void AddEntity(IEntity entity) {
            if (!entity.ContainsAll(system.KeyComponents)) {
                //entity does not have the right components
                return;
            }
            if(entities.Contains(entity)) {
                //system already contains the entity.
                return;
            }
            entities.Add(entity);
            system.EntityAdded(entity);
        }

        public void RemoveEntity(IEntity entity) {
            if (!entities.Contains(entity)) {
                //system does not contain the entity.
                return;
            }
            entities.Remove(entity);
            system.EntityRemoved(entity);
        }

        public void CheckEntityValidity(IEntity entity) {
            if (entities.Contains(entity)) {
                if (!entity.ContainsAll(system.KeyComponents)) {
                    RemoveEntity(entity);
                }
            }
            else {
                if (entity.ContainsAll(system.KeyComponents)) {
                    AddEntity(entity);
                }
            }
        }
    }
}
