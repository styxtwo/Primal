﻿using NUnit.Framework;
using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    [TestFixture]
    public class WorldAdditionTests : BaseTests {
        [Test]
        public void TestEntityAddition() {
            IWorld world = CreateWorld();

            Entity entity = world.CreateEntity();
            AddComponents(entity, new ComponentA());

            Assert.AreEqual(1, world.DebugInfo.TotalEntityCount);
        }
        
        [Test]
        public void TestEntityDifferentDoubleAddition() {
            IWorld world = CreateWorld();

            Entity entity = world.CreateEntity();
            Entity entity2 = world.CreateEntity();

            AddComponents(entity, new ComponentA());
            AddComponents(entity2, new ComponentA());

            Assert.AreEqual(2, world.DebugInfo.TotalEntityCount);
        }

        [Test]
        public void TestEntityRemoval() {
            IWorld world = CreateWorld();

            Entity entity = world.CreateEntity();
            AddComponents(entity, new ComponentA());
            world.RemoveEntity(entity);

            Assert.AreEqual(0, world.DebugInfo.TotalEntityCount);
        }

        [Test]
        public void TestEntityDoubleRemoval() {
            IWorld world = CreateWorld();

            Entity entity = world.CreateEntity();
            AddComponents(entity, new ComponentA());

            world.RemoveEntity(entity);
            world.RemoveEntity(entity);

            Assert.AreEqual(0, world.DebugInfo.TotalEntityCount);
        }

        [Test]
        public void TestDoubleEntitySingleRemoval() {
            IWorld world = CreateWorld();
            Entity entity = world.CreateEntity();
            Entity entity2 = world.CreateEntity();

            AddComponents(entity, new ComponentA());
            AddComponents(entity2, new ComponentA());

            world.RemoveEntity(entity2);

            Assert.AreEqual(1, world.DebugInfo.TotalEntityCount);
        }

        [Test]
        public void TestSystemAddition() {
            IWorld world = CreateWorld();

            world.AddSystem(new SystemA());

            Assert.AreEqual(1, world.DebugInfo.TotalSystemCount);
        }

        [Test]
        public void TestDoubleSystemAddition() {
            IWorld world = CreateWorld();

            world.AddSystem(new SystemA());
            world.AddSystem(new SystemA());

            Assert.AreEqual(1, world.DebugInfo.TotalSystemCount);
        }

        [Test]
        public void TestDoubleSameSystemAddition() {
            IWorld world = CreateWorld();
            BaseSystem system = new SystemA();

            world.AddSystem(system);
            world.AddSystem(system);

            Assert.AreEqual(1, world.DebugInfo.TotalSystemCount);
        }

        [Test]
        public void TestDifferentDoubleSystemAddition() {
            IWorld world = CreateWorld();

            world.AddSystem(new SystemA());
            world.AddSystem(new SystemB());

            Assert.AreEqual(2, world.DebugInfo.TotalSystemCount);
        }
    }
}
