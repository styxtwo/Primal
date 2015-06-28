using Primal.Api;
using System;
using System.Collections.Generic;

namespace Primal {
    /// <summary>
    /// Implementation of the IWorld interface.
    /// </summary>
    class World : IWorld {
        public IDebugInfo DebugInfo { get; private set; }

        private Entities entities;
        private Systems systems;

        public World() {
            entities = new Entities();
            systems = new Systems(entities);
            DebugInfo = new DebugInfo(entities, systems);
        }

        public void AddSystem(BaseSystem system) {
            system.World = this;
            systems.Add(system, entities.Find(system.KeyComponents));
        }

        public Entity CreateEntity() {
            Entity entity = new Entity();
            entities.Add(entity);
            return entity;
        }

        public void RemoveEntity(Entity entity) {
            entities.Remove(entity);
            entity.Dispose();
        }

        public void Update(double elapsedMs, params Type[] excluded) {
            systems.Update(elapsedMs, excluded);
        }

        public void Update<T>(double elapsedMs) {
            systems.Update<T>(elapsedMs);
        }

        public IFinder EntityFinder {
            get {
                return entities;
            }
        }
    }
}
