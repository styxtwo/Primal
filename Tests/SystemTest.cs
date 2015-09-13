using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    [TestFixture]
    public class SystemTest {

        [Test]
        public void TestEmptyKeyComponents() {
            BaseSystem system = new EmptySystem();
            Assert.AreEqual(0, system.KeyComponents.Count);
        }

        [Test]
        public void TestAdditionKeyComponents() {
            BaseSystem system = new EmptySystem();
            system.AddKeyComponent<ComponentA>();
            Assert.AreEqual(1, system.KeyComponents.Count);
        }

        [Test]
        public void TestDoubleAdditionKeyComponents() {
            BaseSystem system = new EmptySystem();
            system.AddKeyComponent<ComponentA>();
            system.AddKeyComponent<ComponentA>();
            Assert.AreEqual(1, system.KeyComponents.Count);
        }

        [Test]
        public void TestDifferentDoubleAdditionKeyComponents() {
            BaseSystem system = new EmptySystem();
            system.AddKeyComponent<ComponentA>();
            system.AddKeyComponent<ComponentB>();
            Assert.AreEqual(2, system.KeyComponents.Count);
        }
    }
}
