using System;

namespace Primal.Api
{
	public interface IEventHandler
	{
		void HandleEvent(IEntityEvent entityEvent);
	}
}

