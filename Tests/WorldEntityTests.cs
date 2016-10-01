using NUnit.Framework;
using Primal.Api;

namespace Primal.Tests
{
	[TestFixture]
	class WorldEntityTests
	{

		UpdateSystem systemA;
		UpdateSystem systemBC;
		IPrimalWorld world;
		IDebugInfo info;

		private void Setup()
		{
			systemA = new SystemA();
			systemBC = new SystemBC();
			world = WorldFactory.Create(systemA, systemBC);
			info = world.DebugInfo;
		}

		[Test]
		public void TestComponentAddition()
		{
			Setup();

			IEntity entity = world.CreateEntity();

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

		[Test]
		public void TestEntityDisposedOnRemoval()
		{
			Setup();

			IEntity entity = world.CreateEntity().Add(new ComponentA());
			world.RemoveEntity(entity);

			Assert.AreEqual(0, entity.ComponentCount);
		}
	}
}
