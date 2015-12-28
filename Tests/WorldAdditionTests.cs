using NUnit.Framework;
using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    [TestFixture]
    class WorldAdditionTests {

        [Test]
        public void TestEntityAddition() {
            IPrimalWorld world = WorldFactory.Create();
            IEntity entity = world.CreateEntity().Add(new ComponentA());

            Assert.AreEqual(1, world.DebugInfo.TotalEntityCount);
        }
        
        [Test]
        public void TestEntityDifferentDoubleAddition() {
            IPrimalWorld world = WorldFactory.Create();

            IEntity entity = world.CreateEntity().Add(new ComponentA());
            IEntity entity2 = world.CreateEntity().Add(new ComponentA());

            Assert.AreEqual(2, world.DebugInfo.TotalEntityCount);
        }

        [Test]
        public void TestEntityRemoval() {
            IPrimalWorld world = WorldFactory.Create();
            IEntity entity = world.CreateEntity().Add(new ComponentA());
            world.RemoveEntity(entity);

            Assert.AreEqual(0, world.DebugInfo.TotalEntityCount);
        }

        [Test]
        public void TestEntityDoubleRemoval() {
            IPrimalWorld world = WorldFactory.Create();
            IEntity entity = world.CreateEntity().Add(new ComponentA());
            world.RemoveEntity(entity);
            world.RemoveEntity(entity);

            Assert.AreEqual(0, world.DebugInfo.TotalEntityCount);
        }

        [Test]
        public void TestDoubleEntitySingleRemoval() {
            IPrimalWorld world = WorldFactory.Create();
            IEntity entity = world.CreateEntity().Add(new ComponentA());
            IEntity entity2 = world.CreateEntity().Add(new ComponentA());

            world.RemoveEntity(entity2);

            Assert.AreEqual(1, world.DebugInfo.TotalEntityCount);
        }

        [Test]
        public void TestSystemAddition() {
            IPrimalWorld world = WorldFactory.Create();

            world.AddSystem(new SystemA());

            Assert.AreEqual(1, world.DebugInfo.TotalSystemCount);
        }

        [Test]
        public void TestDoubleSystemAddition() {
            IPrimalWorld world = WorldFactory.Create();

            world.AddSystem(new SystemA());
            world.AddSystem(new SystemA());

            Assert.AreEqual(1, world.DebugInfo.TotalSystemCount);
        }

        [Test]
        public void TestDoubleSameSystemAddition() {
            IPrimalWorld world = WorldFactory.Create();
            UpdateSystem system = new SystemA();

            world.AddSystem(system);
            world.AddSystem(system);

            Assert.AreEqual(1, world.DebugInfo.TotalSystemCount);
        }

        [Test]
        public void TestDifferentDoubleSystemAddition() {
            IPrimalWorld world = WorldFactory.Create();

            world.AddSystem(new SystemA());
            world.AddSystem(new SystemB());

            Assert.AreEqual(2, world.DebugInfo.TotalSystemCount);
        }

        [Test]
        public void TestParamsSystemAddition() {
            IPrimalWorld world = WorldFactory.Create();

            world.AddSystems(new SystemA(), new SystemB());
            Assert.AreEqual(2, world.DebugInfo.TotalSystemCount);
        }

        [Test]
        public void TestFluentSystemAddition() {
            IPrimalWorld world = WorldFactory.Create();

            world.AddSystem(new SystemA()).AddSystem(new SystemB());
            Assert.AreEqual(2, world.DebugInfo.TotalSystemCount);
        }
    }
}
