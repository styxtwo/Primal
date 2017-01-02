using NUnit.Framework;
using Primal.Api;

namespace Primal.Tests
{
	[TestFixture]
	class EventBusTests
	{
		
		[Test]
		public void TestEventIsRecieved()
		{
			Handler eventHandler = new Handler();
			EventBus eventBus = new EventBus();
			eventBus.Register(eventHandler, 1);
			eventBus.Post(EntityEvent.Create(null, 1));
			Assert.AreEqual(1, eventHandler.EventCount);
		}

		[Test]
		public void TestMultipleEventAreRecieved()
		{
			Handler eventHandler = new Handler();
			EventBus eventBus = new EventBus();
			eventBus.Register(eventHandler, 1);
			eventBus.Post(EntityEvent.Create(null, 1));
			eventBus.Post(EntityEvent.Create(null, 1));
			Assert.AreEqual(2, eventHandler.EventCount);
		}

		[Test]
		public void TestNonSubscribedEventsAreIgnored()
		{
			Handler eventHandler = new Handler();
			EventBus eventBus = new EventBus();
			eventBus.Register(eventHandler, 1);
			eventBus.Post(EntityEvent.Create(null, 2));
			Assert.AreEqual(0, eventHandler.EventCount);
		}

		[Test]
		public void TestEventsAreIgnoredWhenDeregistered()
		{
			Handler eventHandler = new Handler();
			EventBus eventBus = new EventBus();
			eventBus.Register(eventHandler, 1);
			eventBus.Post(EntityEvent.Create(null, 1));
			eventBus.Deregister(eventHandler, 1);
			eventBus.Post(EntityEvent.Create(null, 1));
			Assert.AreEqual(1, eventHandler.EventCount);
		}
	}

	class Handler : IEventHandler
	{
		public int EventCount { get; private set; }

		public void HandleEvent(IEntityEvent entityEvent)
		{
			EventCount ++;
		}
	}
}
