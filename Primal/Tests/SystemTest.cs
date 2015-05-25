using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    [TestClass]
    public class SystemTest {

        [TestMethod]
        public void TestEmptyKeyComponents() {
            BaseSystem system = new EmptySystem();
            Assert.AreEqual(0, system.KeyComponents.Count);
        }

        [TestMethod]
        public void TestAdditionKeyComponents() {
            BaseSystem system = new EmptySystem();
            system.AddKeyComponent<ComponentA>();
            Assert.AreEqual(1, system.KeyComponents.Count);
        }

        [TestMethod]
        public void TestDoubleAdditionKeyComponents() {
            BaseSystem system = new EmptySystem();
            system.AddKeyComponent<ComponentA>();
            system.AddKeyComponent<ComponentA>();
            Assert.AreEqual(1, system.KeyComponents.Count);
        }

        [TestMethod]
        public void TestDifferentDoubleAdditionKeyComponents() {
            BaseSystem system = new EmptySystem();
            system.AddKeyComponent<ComponentA>();
            system.AddKeyComponent<ComponentB>();
            Assert.AreEqual(2, system.KeyComponents.Count);
        }
    }
}
