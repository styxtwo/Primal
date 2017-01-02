using System;
using Primal.Api;

namespace Primal.Api
{
	public interface IEventBus
	{
		void Post(IEntityEvent entityEvent) ;

		void Register(IEventHandler handler, int typeId);
	}
}

