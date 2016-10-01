using NUnit.Framework;
using Primal.Api;

namespace Primal.Tests
{
	[TestFixture]
	class WorldSystemTests
	{

		UpdateSystem systemO;
		UpdateSystem systemA;
		UpdateSystem systemB;
		UpdateSystem systemBC;
		DrawSystem systemD;
		IPrimalWorld world;
		IDebugInfo info;

		void Setup()
		{
			systemO = new SystemE();
			systemA = new SystemA();
			systemB = new SystemB();
			systemBC = new SystemBC();
			systemD = new SystemD();
			world = WorldFactory.Create(systemO, systemA, systemB, systemBC, systemD);
			info = world.DebugInfo;
		}

		[Test]
		public void TestEntityAddition()
		{
			Setup();
			world.CreateEntity().Add(new ComponentA());

			Assert.AreEqual(1, info.EntityCount(systemO));
			Assert.AreEqual(1, info.EntityCount(systemA));
			Assert.AreEqual(0, info.EntityCount(systemB));
			Assert.AreEqual(0, info.EntityCount(systemBC));
			Assert.AreEqual(0, info.EntityCount(systemD));
		}

		[Test]
		public void TestEntityRemoval()
		{
			Setup();

			IEntity entity = world.CreateEntity().Add(new ComponentA());
			world.RemoveEntity(entity);

			Assert.AreEqual(0, info.EntityCount(systemO));
			Assert.AreEqual(0, info.EntityCount(systemA));
			Assert.AreEqual(0, info.EntityCount(systemB));
			Assert.AreEqual(0, info.EntityCount(systemBC));
			Assert.AreEqual(0, info.EntityCount(systemD));
		}

		[Test]
		public void TestMultipleEntities()
		{
			Setup();

			world.CreateEntity().Add(new ComponentA());
			world.CreateEntity().Add(new ComponentA(), new ComponentB());
			world.CreateEntity().Add(new ComponentB(), new ComponentC());
			world.CreateEntity().Add(new ComponentD());

			Assert.AreEqual(4, info.EntityCount(systemO));
			Assert.AreEqual(2, info.EntityCount(systemA));
			Assert.AreEqual(2, info.EntityCount(systemB));
			Assert.AreEqual(1, info.EntityCount(systemBC));
			Assert.AreEqual(1, info.EntityCount(systemD));
		}

		[Test]
		public void TestMoreMultipleEntities()
		{
			Setup();
			IEntity entity = world.CreateEntity().Add(new ComponentA());

			world.CreateEntity().Add(new ComponentA());
			world.CreateEntity().Add(new ComponentA());
			world.CreateEntity().Add(new ComponentB());
			world.CreateEntity().Add(new ComponentC(), new ComponentD());
			world.CreateEntity().Add(new ComponentB(), new ComponentC());
			world.CreateEntity().Add(new ComponentC(), new ComponentB(), new ComponentD());

			world.RemoveEntity(entity);

			Assert.AreEqual(6, info.EntityCount(systemO));
			Assert.AreEqual(2, info.EntityCount(systemA));
			Assert.AreEqual(3, info.EntityCount(systemB));
			Assert.AreEqual(2, info.EntityCount(systemBC));
			Assert.AreEqual(2, info.EntityCount(systemD));
		}

		[Test]
		public void TestSystemAddedAfterEntity()
		{
			world = WorldFactory.Create();
			info = world.DebugInfo;

			world.CreateEntity().Add(new ComponentA());
			world.CreateEntity().Add(new ComponentB(), new ComponentD());
			world.CreateEntity().Add(new ComponentB(), new ComponentC());

			systemO = new SystemE();
			systemA = new SystemA();
			systemB = new SystemB();
			systemBC = new SystemBC();
			systemD = new SystemD();
			world.AddSystem(systemO);
			world.AddSystem(systemA);
			world.AddSystem(systemB);
			world.AddSystem(systemBC);
			world.AddSystem(systemD);

			world.CreateEntity().Add(new ComponentA());

			Assert.AreEqual(4, info.EntityCount(systemO));
			Assert.AreEqual(2, info.EntityCount(systemA));
			Assert.AreEqual(2, info.EntityCount(systemB));
			Assert.AreEqual(1, info.EntityCount(systemBC));
			Assert.AreEqual(1, info.EntityCount(systemD));
		}

		[Test]
		public void TestSystemUpdate()
		{
			world = WorldFactory.Create();
			SystemE e = new SystemE();
			world.AddSystem(e);

			world.Update(1);
			Assert.IsTrue(e.IsUpdated, "Expect E to be updated.");
		}

		[Test]
		public void TestDrawSystemUpdate()
		{
			world = WorldFactory.Create();
			SystemD d = new SystemD();
			world.AddSystem(d);

			world.Draw(1);
			Assert.IsTrue(d.IsUpdated, "Expect D to be updated.");
		}

		[Test]
		public void TestSystemDrawOther()
		{
			world = WorldFactory.Create();
			SystemE e = new SystemE();
			SystemD d = new SystemD();
			world.AddSystem(d);
			world.AddSystem(e);

			world.Draw(1);
			Assert.IsFalse(e.IsUpdated, "Expect E to not be updated, only D is updated.");
		}
	}
}
