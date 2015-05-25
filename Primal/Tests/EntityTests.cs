using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    [TestClass]
    public class EntityTests {

        [TestMethod]
        public void TestComponentAdding() {
            Entity entity = new Entity();
            entity.Add(new ComponentA());
            Assert.AreEqual(1, entity.ComponentCount);
        }

        [TestMethod]
        public void TestComponentRemoval() {
            Entity entity = new Entity();
            entity.Add(new ComponentA());
            entity.Remove<ComponentA>();
            Assert.AreEqual(0, entity.ComponentCount);
        }

        [TestMethod]
        public void TestComponentDoubleAddition() {
            Entity entity = new Entity();
            entity.Add(new ComponentA());
            entity.Add(new ComponentA());
            Assert.AreEqual(1, entity.ComponentCount);
        }

        [TestMethod]
        public void TestComponentDoubleRemoval() {
            Entity entity = new Entity();
            entity.Add(new ComponentA());
            entity.Remove<ComponentA>();
            entity.Remove<ComponentA>();
            Assert.AreEqual(0, entity.ComponentCount);
        }

        [TestMethod]
        public void TestTwoComponentAddition() {
            Entity entity = new Entity();
            entity.Add(new ComponentA());
            entity.Add(new ComponentB());
            Assert.AreEqual(2, entity.ComponentCount);
        }

        [TestMethod]
        public void TestContains() {
            Entity entity = new Entity();
            entity.Add(new ComponentA());
            Assert.AreEqual(true, entity.Contains<ComponentA>());

            entity.Remove<ComponentA>();
            Assert.AreEqual(false, entity.Contains<ComponentA>());
        }
    }
}
