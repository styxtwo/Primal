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

            world.AddEntity(CreateEntity(new ComponentA()));

            Assert.AreEqual(1, info.EntityCount(systemO));
            Assert.AreEqual(1, info.EntityCount(systemA));
            Assert.AreEqual(0, info.EntityCount(systemB));
            Assert.AreEqual(0, info.EntityCount(systemBC));
        }

        [TestMethod]
        public void TestEntityRemoval() {
            Setup();

            Entity entity = CreateEntity(new ComponentA());
            world.AddEntity(entity);
            world.RemoveEntity(entity);

            Assert.AreEqual(0, info.EntityCount(systemO));
            Assert.AreEqual(0, info.EntityCount(systemA));
            Assert.AreEqual(0, info.EntityCount(systemB));
            Assert.AreEqual(0, info.EntityCount(systemBC));
        }

        [TestMethod]
        public void TestMultipleEntities() {
            Setup();

            world.AddEntity(CreateEntity(new ComponentA()));
            world.AddEntity(CreateEntity(new ComponentA(), new ComponentB()));
            world.AddEntity(CreateEntity(new ComponentB(), new ComponentC()));

            Assert.AreEqual(3, info.EntityCount(systemO));
            Assert.AreEqual(2, info.EntityCount(systemA));
            Assert.AreEqual(2, info.EntityCount(systemB));
            Assert.AreEqual(1, info.EntityCount(systemBC));
        }

        [TestMethod]
        public void TestMoreMultipleEntities() {
            Setup();
            Entity entity = CreateEntity(new ComponentA());

            world.AddEntity(entity);
            world.AddEntity(CreateEntity(new ComponentA()));
            world.AddEntity(CreateEntity(new ComponentA()));
            world.AddEntity(CreateEntity(new ComponentB()));
            world.AddEntity(CreateEntity(new ComponentC()));
            world.AddEntity(CreateEntity(new ComponentB(), new ComponentC()));
            world.AddEntity(CreateEntity(new ComponentC(), new ComponentB()));
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

            world.AddEntity(CreateEntity(new ComponentA()));
            world.AddEntity(CreateEntity(new ComponentB()));
            world.AddEntity(CreateEntity(new ComponentB(), new ComponentC()));

            systemO = new EmptySystem();
            systemA = new SystemA();
            systemB = new SystemB();
            systemBC = new SystemBC();
            world.AddSystem(systemO);
            world.AddSystem(systemA);
            world.AddSystem(systemB);
            world.AddSystem(systemBC);

            world.AddEntity(CreateEntity(new ComponentA()));

            Assert.AreEqual(4, info.EntityCount(systemO));
            Assert.AreEqual(2, info.EntityCount(systemA));
            Assert.AreEqual(2, info.EntityCount(systemB));
            Assert.AreEqual(1, info.EntityCount(systemBC));
        }
    }
}
