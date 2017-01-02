using System;

namespace Primal.Api
{
	public class EntityEvent : IEntityEvent
	{
		public static EntityEvent Create(IEntity source, int eventType)
		{
			return new EntityEvent(source, eventType);
		}

		public IEntity Source { private set; get; }

		public int Type { private set; get; }

		public EntityEvent(IEntity source, int eventType)
		{
			Source = source;
			Type = eventType;
		}

		public bool IsType(int type)
		{
			return Type == type;
		}
	}
}

