using NUnit.Framework;

namespace Primal.Tests
{
	[TestFixture]
	class SystemTest
	{

		[Test]
		public void TestEmptyKeyComponents()
		{
			AbstractSystem system = new SystemE();
			Assert.AreEqual(0, system.RegisteredComponents.Count);
		}

		[Test]
		public void TestAdditionKeyComponents()
		{
			AbstractSystem system = new SystemE();
			system.RegisterComponent<ComponentA>();
			Assert.AreEqual(1, system.RegisteredComponents.Count);
		}

		[Test]
		public void TestDoubleAdditionKeyComponents()
		{
			AbstractSystem system = new SystemE();
			system.RegisterComponent<ComponentA>();
			system.RegisterComponent<ComponentA>();
			Assert.AreEqual(1, system.RegisteredComponents.Count);
		}

		[Test]
		public void TestDifferentDoubleAdditionKeyComponents()
		{
			AbstractSystem system = new SystemE();
			system.RegisterComponent<ComponentA>();
			system.RegisterComponent<ComponentB>();
			Assert.AreEqual(2, system.RegisteredComponents.Count);
		}
	}
}
