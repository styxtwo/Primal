using Microsoft.VisualStudio.TestTools.UnitTesting;
using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    [TestClass]
    public class WorldEntityTests : BaseTests {
        BaseSystem systemA;
        BaseSystem systemBC;
        IWorld world;
        IDebugInfo info;

        private void Setup() {
            systemA = new SystemA();
            systemBC = new SystemBC();
            world = CreateWorld(systemA, systemBC);
            info = world.DebugInfo;
        }

        [TestMethod]
        public void TestComponentAddition() {
            Setup();

            Entity entity = world.CreateEntity();

            //no components.
            Assert.AreEqual(0, info.EntityCount(systemA));
            Assert.AreEqual(0, info.EntityCount(systemBC));

            entity.Add(new ComponentA());

            //A
            Assert.AreEqual(1, info.EntityCount(systemA));
            Assert.AreEqual(0, info.EntityCount(systemBC));

            entity.Add(new ComponentB());

            //A + B
            Assert.AreEqual(1, info.EntityCount(systemA));
            Assert.AreEqual(0, info.EntityCount(systemBC));

            entity.Add(new ComponentC());

            //A + B + C
            Assert.AreEqual(1, info.EntityCount(systemA));
            Assert.AreEqual(1, info.EntityCount(systemBC));

            entity.Remove<ComponentA>();

            //B + C
            Assert.AreEqual(0, info.EntityCount(systemA));
            Assert.AreEqual(1, info.EntityCount(systemBC));

            entity.Remove<ComponentC>();

            //B
            Assert.AreEqual(0, info.EntityCount(systemA));
            Assert.AreEqual(0, info.EntityCount(systemBC));

            entity.Add(new ComponentC());

            //B + C
            Assert.AreEqual(0, info.EntityCount(systemA));
            Assert.AreEqual(1, info.EntityCount(systemBC));
        }

        [TestMethod]
        public void TestEntityDisposedOnRemoval() {
            Setup();

            Entity entity = world.CreateEntity();
            AddComponents(entity, new ComponentA());
            world.RemoveEntity(entity);

            Assert.AreEqual(0, entity.ComponentCount);
        }
    }
}
