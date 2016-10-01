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
			Assert.AreEqual(0, system.KeyComponents.Count);
		}

		[Test]
		public void TestAdditionKeyComponents()
		{
			AbstractSystem system = new SystemE();
			system.AddKeyComponent<ComponentA>();
			Assert.AreEqual(1, system.KeyComponents.Count);
		}

		[Test]
		public void TestDoubleAdditionKeyComponents()
		{
			AbstractSystem system = new SystemE();
			system.AddKeyComponent<ComponentA>();
			system.AddKeyComponent<ComponentA>();
			Assert.AreEqual(1, system.KeyComponents.Count);
		}

		[Test]
		public void TestDifferentDoubleAdditionKeyComponents()
		{
			AbstractSystem system = new SystemE();
			system.AddKeyComponent<ComponentA>();
			system.AddKeyComponent<ComponentB>();
			Assert.AreEqual(2, system.KeyComponents.Count);
		}
	}
}
