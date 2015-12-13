using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    abstract class BaseTests {

        public void AddComponents(IEntity entity, params IComponent[] components) {
            foreach (IComponent component in components) {
                entity.Add(component);
            }
        }

        public IPrimalWorld CreateWorld(params BaseSystem[] systems) {
            IPrimalWorld world = WorldFactory.Create();
            foreach (BaseSystem system in systems) {
                world.AddSystem(system);
            }
            return world;
        }
    }
}
