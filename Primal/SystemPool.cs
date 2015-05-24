using Primal.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace Primal {
    class ProcessPool {
        public event Action<Process> SystemAdded;
        public event Action<Process> SystemRemoved;

        private IList<Process> systems;

        public ProcessPool() {
            systems = new List<Process>();
        }

        public void Add(Process system) {
            systems.Add(system);
            SystemAdded.NullSafeInvoke(system);
        }

        public bool Remove(Process system) {
            bool success = systems.Remove(system);
            SystemRemoved.NullSafeInvoke(system);
            return success;
        }

        public void AddEntity(IEntity entity) {
            foreach (Process system in systems) {
                system.AddEntity(entity);
            }
        }

        public void RemoveEntity(IEntity entity) {
            foreach (Process system in systems) {
                system.RemoveEntity(entity);
            }
        }

        public void EntityChanged(IEntity entity) {
            foreach (Process system in systems) {
                system.ChangeEntity(entity);
            }
        }

        internal void Update(double elapsedMs) {
            foreach (Process system in systems) {
                system.Update(elapsedMs);
            }
        }
    }
}
