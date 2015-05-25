using Microsoft.VisualStudio.TestTools.UnitTesting;
using Primal.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    [TestClass]
    public class WorldAdditionTests : BaseTests {
        [TestMethod]
        public void TestEntityAddition() {
            IWorld world = CreateWorld();
            Entity entity = CreateEntity(new ComponentA());

            world.AddEntity(entity);

            Assert.AreEqual(1, world.DebugInfo.TotalEntityCount);
        }

        [TestMethod]
        public void TestEntityDoubleAddition() {
            IWorld world = CreateWorld();
            Entity entity = CreateEntity(new ComponentA());

            world.AddEntity(entity);
            world.AddEntity(entity);

            Assert.AreEqual(1, world.DebugInfo.TotalEntityCount);
        }

        [TestMethod]
        public void TestEntityDifferentDoubleAddition() {
            IWorld world = CreateWorld();
            Entity entity = CreateEntity(new ComponentA());
            Entity entity2 = CreateEntity(new ComponentA());

            world.AddEntity(entity);
            world.AddEntity(entity2);

            Assert.AreEqual(2, world.DebugInfo.TotalEntityCount);
        }

        [TestMethod]
        public void TestEntityRemoval() {
            IWorld world = CreateWorld();
            Entity entity = CreateEntity(new ComponentA());

            world.AddEntity(entity);
            world.RemoveEntity(entity);

            Assert.AreEqual(0, world.DebugInfo.TotalEntityCount);
        }

        [TestMethod]
        public void TestEntityDoubleRemoval() {
            IWorld world = CreateWorld();
            Entity entity = CreateEntity(new ComponentA());

            world.AddEntity(entity);
            world.RemoveEntity(entity);
            world.RemoveEntity(entity);

            Assert.AreEqual(0, world.DebugInfo.TotalEntityCount);
        }

        [TestMethod]
        public void TestDoubleEntitySingleRemoval() {
            IWorld world = CreateWorld();
            Entity entity = CreateEntity(new ComponentA());
            Entity entity2 = CreateEntity(new ComponentA());

            world.AddEntity(entity);
            world.AddEntity(entity2);
            world.RemoveEntity(entity2);

            Assert.AreEqual(1, world.DebugInfo.TotalEntityCount);
        }

        [TestMethod]
        public void TestSystemAddition() {
            IWorld world = CreateWorld();

            world.AddSystem(new SystemA());

            Assert.AreEqual(1, world.DebugInfo.TotalSystemCount);
        }

        [TestMethod]
        public void TestDoubleSystemAddition() {
            IWorld world = CreateWorld();

            world.AddSystem(new SystemA());
            world.AddSystem(new SystemA());

            Assert.AreEqual(1, world.DebugInfo.TotalSystemCount);
        }

        [TestMethod]
        public void TestDoubleSameSystemAddition() {
            IWorld world = CreateWorld();
            BaseSystem system = new SystemA();

            world.AddSystem(system);
            world.AddSystem(system);

            Assert.AreEqual(1, world.DebugInfo.TotalSystemCount);
        }

        [TestMethod]
        public void TestDifferentDoubleSystemAddition() {
            IWorld world = CreateWorld();

            world.AddSystem(new SystemA());
            world.AddSystem(new SystemB());

            Assert.AreEqual(2, world.DebugInfo.TotalSystemCount);
        }
    }
}
