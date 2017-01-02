using NUnit.Framework;
using Primal.Api;

namespace Primal.Tests
{
	[TestFixture]
	class WorldUpdateTests
	{

		[Test]
		public void TestMethodsCalled_NoEntity()
		{
			UpdatableSystem system = new UpdatableSystem();
			IPrimalWorld world = WorldFactory.Create(system);

			world.Update(0);

			Assert.AreEqual(0, system.EntityAddedCalled);
			Assert.AreEqual(0, system.EntityRemovedCalled);
			Assert.AreEqual(1, system.BeforeUpdateCalled);
			Assert.AreEqual(1, system.AfterUpdateCalled);
			Assert.AreEqual(0, system.UpdateEntityCalled);
		}

		[Test]
		public void TestMethodsCalled_OneEntity()
		{
			UpdatableSystem system = new UpdatableSystem();
			IPrimalWorld world = WorldFactory.Create(system);

			world.CreateEntity();
			Assert.AreEqual(1, system.EntityAddedCalled);

			world.Update(0);

			Assert.AreEqual(1, system.EntityAddedCalled);
			Assert.AreEqual(0, system.EntityRemovedCalled);
			Assert.AreEqual(1, system.BeforeUpdateCalled);
			Assert.AreEqual(1, system.AfterUpdateCalled);
			Assert.AreEqual(1, system.UpdateEntityCalled);
		}

		[Test]
		public void TestMethodsCalled_OneEntityRemoved()
		{
			UpdatableSystem system = new UpdatableSystem();
			IPrimalWorld world = WorldFactory.Create(system);
			IEntity entity = world.CreateEntity();
			world.RemoveEntity(entity);

			Assert.AreEqual(1, system.EntityAddedCalled);
			Assert.AreEqual(1, system.EntityRemovedCalled);

			world.Update(0);

			Assert.AreEqual(1, system.EntityAddedCalled);
			Assert.AreEqual(1, system.EntityRemovedCalled);
			Assert.AreEqual(1, system.BeforeUpdateCalled);
			Assert.AreEqual(1, system.AfterUpdateCalled);
			Assert.AreEqual(0, system.UpdateEntityCalled);
		}

		[Test]
		public void TestMethodsCalled_TwoUpdates()
		{
			UpdatableSystem system = new UpdatableSystem();
			IPrimalWorld world = WorldFactory.Create(system);
			world.CreateEntity();

			world.Update(0);
			world.Update(0);

			Assert.AreEqual(1, system.EntityAddedCalled);
			Assert.AreEqual(0, system.EntityRemovedCalled);
			Assert.AreEqual(2, system.BeforeUpdateCalled);
			Assert.AreEqual(2, system.AfterUpdateCalled);
			Assert.AreEqual(2, system.UpdateEntityCalled);
		}

		[Test]
		public void TestMethodsCalled_TwoUpdates_TwoEntities()
		{
			UpdatableSystem system = new UpdatableSystem();
			IPrimalWorld world = WorldFactory.Create(system);
			world.CreateEntity();
			IEntity entity2 = world.CreateEntity();

			world.Update(0);

			Assert.AreEqual(2, system.EntityAddedCalled);
			Assert.AreEqual(0, system.EntityRemovedCalled);
			Assert.AreEqual(1, system.BeforeUpdateCalled);
			Assert.AreEqual(1, system.AfterUpdateCalled);
			Assert.AreEqual(2, system.UpdateEntityCalled);

			world.RemoveEntity(entity2);
			world.Update(0);

			Assert.AreEqual(2, system.EntityAddedCalled);
			Assert.AreEqual(1, system.EntityRemovedCalled);
			Assert.AreEqual(2, system.BeforeUpdateCalled);
			Assert.AreEqual(2, system.AfterUpdateCalled);
			Assert.AreEqual(3, system.UpdateEntityCalled);
		}

		[Test]
		public void TestMethodsCalled_FiveUpdates_TwoEntities()
		{
			UpdatableSystem system = new UpdatableSystem();
			IPrimalWorld world = WorldFactory.Create(system);
			world.CreateEntity();
			world.CreateEntity();

			world.Update(0);
			world.Update(0);
			world.Update(0);
			world.Update(0);
			world.Update(0);

			Assert.AreEqual(2, system.EntityAddedCalled);
			Assert.AreEqual(0, system.EntityRemovedCalled);
			Assert.AreEqual(5, system.BeforeUpdateCalled);
			Assert.AreEqual(5, system.AfterUpdateCalled);
			Assert.AreEqual(10, system.UpdateEntityCalled);
		}
	}
}
