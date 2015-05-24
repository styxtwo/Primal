using Primal.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace Primal {
    class SystemPool {
        private IList<SystemHandler> systems;

        public SystemPool() {
            systems = new List<SystemHandler>();
        }

        public void Add(ISystem system) {
            systems.Add(new SystemHandler(system));
        }

        public bool Remove(SystemHandler system) {
            bool success = systems.Remove(system);
            return success;
        }

        public void AddEntity(IEntity entity) {
            foreach (SystemHandler system in systems) {
                system.AddEntity(entity);
            }
        }

        public void RemoveEntity(IEntity entity) {
            foreach (SystemHandler system in systems) {
                system.RemoveEntity(entity);
            }
        }

        public void EntityChanged(IEntity entity) {
            foreach (SystemHandler system in systems) {
                system.ChangeEntity(entity);
            }
        }

        internal void Update(double elapsedMs) {
            foreach (SystemHandler system in systems) {
                system.Update(elapsedMs);
            }
        }
    }
}
