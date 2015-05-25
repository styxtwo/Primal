using Primal.API;

namespace Primal {
    /// <summary>
    /// Implementation of the IWorld interface.
    /// </summary>
    public class World : IWorld {
        public IFinder EntityFinder { get; private set; }
        public IDebugInfo DebugInfo { get; private set; }

        private Entities entities;
        private Systems systems;

        public World() {
            entities = new Entities();
            systems = new Systems(entities);
            EntityFinder = new EntityFinder(entities);
            DebugInfo = new DebugInfo(entities, systems);
        }

        public void AddSystem(ISystem system) {
            system.World = this;
            systems.Add(system);
        }

        public void AddEntity(IEntity entity) {
            entities.Add(entity);
        }

        public void RemoveEntity(IEntity entity) {
            entities.Remove(entity);
            entity.Dispose();
        }

        public void Update(double elapsedMs) {
            systems.Update(elapsedMs);
        }
    }
}
