using Microsoft.VisualStudio.TestTools.UnitTesting;
using Primal.Api;

namespace Primal.Tests {
    [TestClass]
    public class WorldSystemTests : BaseTests{
        BaseSystem systemO;
        BaseSystem systemA;
        BaseSystem systemB;
        BaseSystem systemBC;
        IWorld world;
        IDebugInfo info;

        private void Setup() {
            systemO = new EmptySystem();
            systemA = new SystemA();
            systemB = new SystemB();
            systemBC = new SystemBC();
            world = CreateWorld(systemO, systemA, systemB, systemBC);
            info = world.DebugInfo;
        }

        [TestMethod]
        public void TestEntityAddition() {
            Setup();

            AddComponents(world.CreateEntity(), new ComponentA());

            Assert.AreEqual(1, info.EntityCount(systemO));
            Assert.AreEqual(1, info.EntityCount(systemA));
            Assert.AreEqual(0, info.EntityCount(systemB));
            Assert.AreEqual(0, info.EntityCount(systemBC));
        }

        [TestMethod]
        public void TestEntityRemoval() {
            Setup();

            Entity entity = world.CreateEntity();
            AddComponents(entity, new ComponentA());
            world.RemoveEntity(entity);

            Assert.AreEqual(0, info.EntityCount(systemO));
            Assert.AreEqual(0, info.EntityCount(systemA));
            Assert.AreEqual(0, info.EntityCount(systemB));
            Assert.AreEqual(0, info.EntityCount(systemBC));
        }

        [TestMethod]
        public void TestMultipleEntities() {
            Setup();

            AddComponents(world.CreateEntity(), new ComponentA());
            AddComponents(world.CreateEntity(), new ComponentA(), new ComponentB());
            AddComponents(world.CreateEntity(), new ComponentB(), new ComponentC());

            Assert.AreEqual(3, info.EntityCount(systemO));
            Assert.AreEqual(2, info.EntityCount(systemA));
            Assert.AreEqual(2, info.EntityCount(systemB));
            Assert.AreEqual(1, info.EntityCount(systemBC));
        }

        [TestMethod]
        public void TestMoreMultipleEntities() {
            Setup();
            Entity entity = world.CreateEntity();
            AddComponents(entity, new ComponentA());
            
            AddComponents(world.CreateEntity(), new ComponentA());
            AddComponents(world.CreateEntity(), new ComponentA());
            AddComponents(world.CreateEntity(), new ComponentB());
            AddComponents(world.CreateEntity(), new ComponentC());
            AddComponents(world.CreateEntity(), new ComponentB(), new ComponentC());
            AddComponents(world.CreateEntity(), new ComponentC(), new ComponentB());

            world.RemoveEntity(entity);

            Assert.AreEqual(6, info.EntityCount(systemO));
            Assert.AreEqual(2, info.EntityCount(systemA));
            Assert.AreEqual(3, info.EntityCount(systemB));
            Assert.AreEqual(2, info.EntityCount(systemBC));
        }

        [TestMethod]
        public void TestSystemAddedAfterEntity() {
            world = CreateWorld();
            info = world.DebugInfo;

            AddComponents(world.CreateEntity(), new ComponentA());
            AddComponents(world.CreateEntity(), new ComponentB());
            AddComponents(world.CreateEntity(), new ComponentB(), new ComponentC());

            systemO = new EmptySystem();
            systemA = new SystemA();
            systemB = new SystemB();
            systemBC = new SystemBC();
            world.AddSystem(systemO);
            world.AddSystem(systemA);
            world.AddSystem(systemB);
            world.AddSystem(systemBC);

            AddComponents(world.CreateEntity(), new ComponentA());

            Assert.AreEqual(4, info.EntityCount(systemO));
            Assert.AreEqual(2, info.EntityCount(systemA));
            Assert.AreEqual(2, info.EntityCount(systemB));
            Assert.AreEqual(1, info.EntityCount(systemBC));
        }

        [TestMethod]
        public void TestSystemUpdate() {
            world = CreateWorld();
            SystemU u = new SystemU();
            world.AddSystem(u);

            world.UpdateAll(1);
            Assert.AreEqual(true, u.IsUpdated, "Expect U to be updated.");
        }

        [TestMethod]
        public void TestSystemExcludedUpdate() {
            world = CreateWorld();
            SystemU u = new SystemU();
            world.AddSystem(u);

            world.UpdateAll(1, typeof(SystemU));
            Assert.AreEqual(false, u.IsUpdated, "Expect U to be excluded, and thus not updated.");
        }

        [TestMethod]
        public void TestSystemUpdateSingle() {
            world = CreateWorld();
            SystemU u = new SystemU();
            world.AddSystem(u);

            world.Update<SystemU>(1);
            Assert.AreEqual(true, u.IsUpdated, "Expect U to be updated.");
        }

        [TestMethod]
        public void TestSystemUpdateSingleOther() {
            world = CreateWorld();
            SystemU u = new SystemU();
            world.AddSystem(u);

            world.Update<SystemA>(1);
            Assert.AreEqual(false, u.IsUpdated, "Expect U to not be updated, only A is updated.");
        }
    }
}
