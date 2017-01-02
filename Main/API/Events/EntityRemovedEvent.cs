using System;

namespace Primal.Api
{
	public class EntityRemovedEvent : AbstractEntityEvent
	{
		public EntityRemovedEvent(IEntity source) 
			: base(source, EntityEventTypes.EntityRemoved)
		{
		}
	}
}

