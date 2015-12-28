using NUnit.Framework;
using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    [TestFixture]
    class DebugInfoTests {

        /// <summary>
        /// The info class under test.
        /// </summary>
        IDebugInfo info;

        IPrimalWorld world;
        IEntity entity;
        UpdateSystem systemA;

        private void Setup() {
            //Systems
            systemA = new SystemA();
            world = WorldFactory.Create(systemA, new SystemB(), new SystemBC(), new SystemE());
            world.AddSystem(new SystemD());

            //Entities
            entity = world.CreateEntity();
            entity.Add(new ComponentA());
            entity.Add(new ComponentB());
            entity.Add(new ComponentC());
            world.CreateEntity().Add(new ComponentA());
            world.CreateEntity();

            info = world.DebugInfo;
        }

        [Test]
        public void TestBaseSystemCount() {
            Setup();
            Assert.AreEqual(4, info.BaseSystemCount, "Expect base systems to be equal.");
        }

        [Test]
        public void TestDrawSystemCount() {
            Setup();
            Assert.AreEqual(1, info.DrawSystemCount, "Expect draw systems to be equal.");
        }

        [Test]
        public void TestTotalSystemCount() {
            Setup();
            Assert.AreEqual(5, info.TotalSystemCount, "Expect systems to be equal.");
        }

        [Test]
        public void TestTotalEntityCount() {
            Setup();
            Assert.AreEqual(3, info.TotalEntityCount, "Expect entity count to be correct.");
        }

        [Test]
        public void TestTotalComponentCount() {
            Setup();
            Assert.AreEqual(4, info.TotalComponentCount, "Expect component count to be correct.");
        }

        [Test]
        public void TestComponentCountForSpecificEntity() {
            Setup();
            Assert.AreEqual(3, info.ComponentCount(entity), "Expect component count to be correct.");
        }

        [Test]
        public void TestEntityCountForSpecificSystem() {
            Setup();
            Assert.AreEqual(2, info.EntityCount(systemA), "Expect entity count to be correct.");
        }

        [Test]
        public void TestBaseSystemOrderisCorrect() {
            Setup();
            IList<Type> expected = new List<Type>();
            expected.Add(typeof(SystemA));
            expected.Add(typeof(SystemB));
            expected.Add(typeof(SystemBC));
            expected.Add(typeof(SystemE));
            Assert.IsTrue(info.UpdateSystemTypes.SequenceEqual(expected), "Expect the order of the systems to be the same.");
        }

        [Test]
        public void TestDrawSystemOrderisCorrect() {
            Setup();
            IList<Type> expected = new List<Type>();
            expected.Add(typeof(SystemD));
            Assert.IsTrue(info.DrawSystemTypes.SequenceEqual(expected), "Expect the order of the systems to be the same.");
        }
    }

}
