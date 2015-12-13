using NUnit.Framework;
using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    [TestFixture]
    class WorldAdditionTests : BaseTests {
        [Test]
        public void TestEntityAddition() {
            IPrimalWorld world = CreateWorld();

            IEntity entity = world.CreateEntity();
            AddComponents(entity, new ComponentA());

            Assert.AreEqual(1, world.DebugInfo.TotalEntityCount);
        }
        
        [Test]
        public void TestEntityDifferentDoubleAddition() {
            IPrimalWorld world = CreateWorld();

            IEntity entity = world.CreateEntity();
            IEntity entity2 = world.CreateEntity();

            AddComponents(entity, new ComponentA());
            AddComponents(entity2, new ComponentA());

            Assert.AreEqual(2, world.DebugInfo.TotalEntityCount);
        }

        [Test]
        public void TestEntityRemoval() {
            IPrimalWorld world = CreateWorld();

            IEntity entity = world.CreateEntity();
            AddComponents(entity, new ComponentA());
            world.RemoveEntity(entity);

            Assert.AreEqual(0, world.DebugInfo.TotalEntityCount);
        }

        [Test]
        public void TestEntityDoubleRemoval() {
            IPrimalWorld world = CreateWorld();

            IEntity entity = world.CreateEntity();
            AddComponents(entity, new ComponentA());

            world.RemoveEntity(entity);
            world.RemoveEntity(entity);

            Assert.AreEqual(0, world.DebugInfo.TotalEntityCount);
        }

        [Test]
        public void TestDoubleEntitySingleRemoval() {
            IPrimalWorld world = CreateWorld();
            IEntity entity = world.CreateEntity();
            IEntity entity2 = world.CreateEntity();

            AddComponents(entity, new ComponentA());
            AddComponents(entity2, new ComponentA());

            world.RemoveEntity(entity2);

            Assert.AreEqual(1, world.DebugInfo.TotalEntityCount);
        }

        [Test]
        public void TestSystemAddition() {
            IPrimalWorld world = CreateWorld();

            world.AddSystem(new SystemA());

            Assert.AreEqual(1, world.DebugInfo.TotalSystemCount);
        }

        [Test]
        public void TestDoubleSystemAddition() {
            IPrimalWorld world = CreateWorld();

            world.AddSystem(new SystemA());
            world.AddSystem(new SystemA());

            Assert.AreEqual(1, world.DebugInfo.TotalSystemCount);
        }

        [Test]
        public void TestDoubleSameSystemAddition() {
            IPrimalWorld world = CreateWorld();
            BaseSystem system = new SystemA();

            world.AddSystem(system);
            world.AddSystem(system);

            Assert.AreEqual(1, world.DebugInfo.TotalSystemCount);
        }

        [Test]
        public void TestDifferentDoubleSystemAddition() {
            IPrimalWorld world = CreateWorld();

            world.AddSystem(new SystemA());
            world.AddSystem(new SystemB());

            Assert.AreEqual(2, world.DebugInfo.TotalSystemCount);
        }
    }
}
