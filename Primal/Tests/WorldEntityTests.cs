﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Primal.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    [TestClass]
    public class WorldEntityTests : BaseTest {
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

            Entity entity = CreateEntity();
            world.AddEntity(entity);

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

            Entity entity = CreateEntity(new ComponentA());
            world.AddEntity(entity);
            world.RemoveEntity(entity);

            Assert.AreEqual(0, entity.ComponentCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUnableToAddDisposedEntity() {
            Setup();

            Entity entity = CreateEntity(new ComponentA());
            world.AddEntity(entity);
            world.RemoveEntity(entity);
            world.AddEntity(entity);
        }
    }
}
