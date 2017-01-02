using System;
using System.Collections.Generic;
using Primal.Api;

namespace Primal
{
	//TODO write tests
	//# should entity removed events be send via these as well?
	class EventBus : IEventBus
	{

		IDictionary<int, IList<IEventHandler>> handlers 
			= new Dictionary<int, IList<IEventHandler>>();

		public void Post(IEntityEvent entityEvent)
		{
			int type = entityEvent.Type;
			if (!handlers.ContainsKey(type)) {
				return;
			}
			foreach (AbstractSystem handler in handlers[type]) {
				handler.HandleEvent(entityEvent);
			}
		}

		public void Register(IEventHandler handler, int typeId)
		{
			if (!handlers.ContainsKey(typeId)) {
				handlers.Add(typeId, new List<IEventHandler>());
			}
			IList<IEventHandler> list = handlers[typeId];
			list.Add(handler);
		}
	}
}

