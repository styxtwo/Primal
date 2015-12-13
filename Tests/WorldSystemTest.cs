using NUnit.Framework;
using Primal.Api;

namespace Primal.Tests {
    [TestFixture]
    class WorldSystemTests : BaseTests {
        BaseSystem systemO;
        BaseSystem systemA;
        BaseSystem systemB;
        BaseSystem systemBC;
        IPrimalWorld world;
        IDebugInfo info;

        private void Setup() {
            systemO = new SystemE();
            systemA = new SystemA();
            systemB = new SystemB();
            systemBC = new SystemBC();
            world = CreateWorld(systemO, systemA, systemB, systemBC);
            info = world.DebugInfo;
        }

        [Test]
        public void TestEntityAddition() {
            Setup();

            AddComponents(world.CreateEntity(), new ComponentA());

            Assert.AreEqual(1, info.EntityCount(systemO));
            Assert.AreEqual(1, info.EntityCount(systemA));
            Assert.AreEqual(0, info.EntityCount(systemB));
            Assert.AreEqual(0, info.EntityCount(systemBC));
        }

        [Test]
        public void TestEntityRemoval() {
            Setup();

            IEntity entity = world.CreateEntity();
            AddComponents(entity, new ComponentA());
            world.RemoveEntity(entity);

            Assert.AreEqual(0, info.EntityCount(systemO));
            Assert.AreEqual(0, info.EntityCount(systemA));
            Assert.AreEqual(0, info.EntityCount(systemB));
            Assert.AreEqual(0, info.EntityCount(systemBC));
        }

        [Test]
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

        [Test]
        public void TestMoreMultipleEntities() {
            Setup();
            IEntity entity = world.CreateEntity();
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

        [Test]
        public void TestSystemAddedAfterEntity() {
            world = CreateWorld();
            info = world.DebugInfo;

            AddComponents(world.CreateEntity(), new ComponentA());
            AddComponents(world.CreateEntity(), new ComponentB());
            AddComponents(world.CreateEntity(), new ComponentB(), new ComponentC());

            systemO = new SystemE();
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

        [Test]
        public void TestSystemUpdate() {
            world = CreateWorld();
            SystemE e = new SystemE();
            world.AddSystem(e);

            world.Update(1);
            Assert.IsTrue(e.IsUpdated, "Expect E to be updated.");
        }

        [Test]
        public void TestDrawSystemUpdate() {
            world = CreateWorld();
            SystemD d = new SystemD();
            world.AddSystem(d);

            world.Draw(1);
            Assert.IsTrue(d.IsUpdated, "Expect D to be updated.");
        }

        [Test]
        public void TestSystemDrawOther() {
            world = CreateWorld();
            SystemE e = new SystemE();
            SystemD d = new SystemD();
            world.AddSystem(d);
            world.AddSystem(e);

            world.Draw(1);
            Assert.IsFalse(e.IsUpdated, "Expect E to not be updated, only D is updated.");
        }
    }
}
