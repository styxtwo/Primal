using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    [TestFixture]
    class EntityTests {

        [Test]
        public void TestComponentAdding() {
            Entity entity = new Entity();
            entity.Add(new ComponentA());
            Assert.AreEqual(1, entity.ComponentCount);
        }

        [Test]
        public void TestComponentRemoval() {
            Entity entity = new Entity();
            entity.Add(new ComponentA());
            entity.Remove<ComponentA>();
            Assert.AreEqual(0, entity.ComponentCount);
        }

        [Test]
        public void TestComponentDoubleAddition() {
            Entity entity = new Entity();
            entity.Add(new ComponentA());
            entity.Add(new ComponentA());
            Assert.AreEqual(1, entity.ComponentCount);
        }

        [Test]
        public void TestComponentDoubleRemoval() {
            Entity entity = new Entity();
            entity.Add(new ComponentA());
            entity.Remove<ComponentA>();
            entity.Remove<ComponentA>();
            Assert.AreEqual(0, entity.ComponentCount);
        }

        [Test]
        public void TestTwoComponentAddition() {
            Entity entity = new Entity();
            entity.Add(new ComponentA());
            entity.Add(new ComponentB());
            Assert.AreEqual(2, entity.ComponentCount);
        }

        [Test]
        public void TestContains() {
            Entity entity = new Entity();
            entity.Add(new ComponentA());
            Assert.AreEqual(true, entity.Contains<ComponentA>());

            entity.Remove<ComponentA>();
            Assert.AreEqual(false, entity.Contains<ComponentA>());
        }
    }
}
