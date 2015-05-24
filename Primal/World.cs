using Primal.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal {
    public class World : IWorld {
        EntityPool entities;
        SystemPool processes;

        public World() {
            entities = new EntityPool();
            entities.EntityChanged += EntityChanged;
            processes = new SystemPool();
        }

        public void AddSystem(ISystem process) {
            processes.Add(process);
        }

        public void AddEntity(IEntity entity) {
            entities.Add(entity);
            processes.AddEntity(entity);
        }

        public void RemoveEntity(IEntity entity) {
            entities.Remove(entity);
            processes.RemoveEntity(entity);
        }

        void EntityChanged(IEntity entity) {
            processes.EntityChanged(entity);
        }

        public void Update(double elapsedMs) {
            processes.Update(elapsedMs);
        }
    }
}
