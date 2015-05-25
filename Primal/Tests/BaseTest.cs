using Primal.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    public abstract class BaseTest {

        public Entity CreateEntity(params Component[] components) {
            Entity entity = new Entity();
            foreach (Component component in components) {
                entity.Add(component);
            }
            return entity;
        }

        public IWorld CreateWorld(params BaseSystem[] systems) {
            IWorld world = WorldFactory.Create();
            foreach (BaseSystem system in systems) {
                world.AddSystem(system);
            }
            return world;
        }
    }
}
