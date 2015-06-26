using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    public abstract class BaseTests {

        public void AddComponents(Entity entity, params Component[] components) {
            foreach (Component component in components) {
                entity.Add(component);
            }
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
