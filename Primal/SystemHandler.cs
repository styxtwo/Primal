using Primal.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal {
    class SystemHandler {
        private IList<IEntity> entities;
        private ISystem system;
        public SystemHandler(ISystem system) {
            this.system = system;
            entities = new List<IEntity>();
        }

        public void Update(double elapsedMs) {
            system.BeforeUpdate();
            foreach (IEntity entity in entities.ToList()) {
                system.UpdateEntity(entity);
            }
            system.AfterUpdate();
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

        public void ChangeEntity(IEntity entity) {
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
