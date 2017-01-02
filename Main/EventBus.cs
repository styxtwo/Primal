using System;
using System.Collections.Generic;
using Primal.Api;

namespace Primal
{
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
			foreach (IEventHandler handler in handlers[type]) {
				handler.HandleEvent(entityEvent);
			}
		}

		public void Register(IEventHandler handler, int typeId)
		{
			if (!handlers.ContainsKey(typeId)) {
				handlers.Add(typeId, new List<IEventHandler>());
			}
			handlers[typeId].Add(handler);
		}

		public void Deregister(IEventHandler handler, int typeId)
		{
			if (!handlers.ContainsKey(typeId)) {
				return;
			}
			handlers[typeId].Remove(handler);
		}
	}
}

