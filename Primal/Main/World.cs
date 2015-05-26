﻿using Primal.Api;
using System;

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

        public void AddEntity(Entity entity) {
            if (entity.IsDisposed) {
                throw (new ArgumentException("Can't add disposed entity."));
            }
            entities.Add(entity);
        }

        public void RemoveEntity(Entity entity) {
            entities.Remove(entity);
            entity.Dispose();
        }

        public void Update(double elapsedMs) {
            systems.Update(elapsedMs);
        }

        public IFinder EntityFinder {
            get {
                return entities;
            }
        }
    }
}