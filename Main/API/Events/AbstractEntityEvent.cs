using System;

namespace Primal.Api
{
	public class AbstractEntityEvent : IEntityEvent
	{
		public IEntity Source { private set; get; }

		public int Type { private set; get; }

		public AbstractEntityEvent(IEntity source, int eventType)
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

