using Primal.Api;
using Primal.Main;
using System;
using System.Collections.Generic;

namespace Primal {
    /// <summary>
    /// Implementation of the IWorld interface.
    /// </summary>
    class World : IPrimalWorld {
        public IDebugInfo DebugInfo { get; private set; }

        private Entities entities;
        private Systems systems;

        public World() {
            entities = new Entities();
            systems = new Systems(entities);
            DebugInfo = new DebugInfo(entities, systems);
        }

        public void AddSystem(BaseSystem system) {
            system.Finder = this.EntityFinder;
            systems.Add(system, entities.EntityFinder.Find(system.KeyComponents));
        }

        public void AddSystem(DrawSystem system) {
            system.Finder = this.EntityFinder;
            systems.Add(system, entities.EntityFinder.Find(system.KeyComponents));
        }

        public IEntity CreateEntity() {
            Entity entity = new Entity();
            entities.Add(entity);
            return entity;
        }

        public void RemoveEntity(IEntity entity) {
            Entity converted_entity = entity as Entity;
            if (converted_entity != null) {
                converted_entity.Dispose();
                entities.Remove(converted_entity);
            }
        }

        public void Update(double elapsedMs) {
            systems.Update(elapsedMs);
        }

        public void Draw(double elapsedMs) {
            systems.Draw(elapsedMs);
        }


        public IFinder EntityFinder {
            get {
                return entities.EntityFinder;
            }
        }
    }
}
