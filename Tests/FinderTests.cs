using NUnit.Framework;
using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    [TestFixture]
    class FinderTests {

        IFinder finder;
        IPrimalWorld world;

        private void Setup() {
            world = WorldFactory.Create(new SystemA(), new SystemBC());
            finder = world.EntityFinder;

            IEntity entity = world.CreateEntity();
            entity.Add(new ComponentA());
            entity.Add(new ComponentB());

            entity = world.CreateEntity();
            entity.Add(new ComponentA());

            entity = world.CreateEntity();
            entity.Add(new ComponentA());

            entity = world.CreateEntity();
            entity.Add(new ComponentB());

            entity = world.CreateEntity();
            entity.Add(new ComponentB());

            entity = world.CreateEntity();
            entity.Add(new ComponentA());
            entity.Add(new ComponentB());

            entity = world.CreateEntity();
            entity.Add(new ComponentA());
            entity.Add(new ComponentB());
        }

        [Test]
        public void TestFindAll_OneComponent() {
            Setup();

            var entities = finder.Find<ComponentA>();
            Assert.AreEqual(entities.Count(), 5, "Expect 5 entities with Component A");
            foreach (Entity entity in entities) {
                Assert.IsTrue(entity.Contains<ComponentA>(), "Expect to contains Component A");
            }
        }

        [Test]
        public void TestFindAll_MultipleComponent() {
            Setup();

            var entities = finder.Find<ComponentA, ComponentB>();
            Assert.AreEqual(entities.Count(), 3, "Expect 3 entities with Component A and Component B");
            foreach(Entity entity in entities) {
                Assert.IsTrue(entity.Contains<ComponentA>(), "Expect to contains Component A");
                Assert.IsTrue(entity.Contains<ComponentB>(), "Expect to contains Component B");
            }
        }

        [Test]
        public void TestFindAll_None() {
            Setup();

            var entities = finder.Find<ComponentA, ComponentB, ComponentC>();
            Assert.AreEqual(entities.Count(), 0, "Expect no entities with Component C");
        }

        [Test]
        public void TestFindFirst_OneComponent() {
            Setup();

            var entity = finder.FindFirst<ComponentA>();
            Assert.IsNotNull(entity, "Expect entity to not be null");
            Assert.IsTrue(entity.Contains<ComponentA>(), "Expect to contains Component A");
        }

        [Test]
        public void TestFindFirst_TwoComponents() {
            Setup();

            var entity = finder.FindFirst<ComponentA, ComponentB>();
            Assert.IsNotNull(entity, "Expect entity to not be null");
            Assert.IsTrue(entity.Contains<ComponentA>(), "Expect to contains Component A");
            Assert.IsTrue(entity.Contains<ComponentB>(), "Expect to contains Component B");
        }

        [Test]
        public void TestFindFirst_None() {
            Setup();

            var entity = finder.FindFirst<ComponentC>();
            Assert.IsNull(entity, "Expect entity to be null");
        }
    }
}
