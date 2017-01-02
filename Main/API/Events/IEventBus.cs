using System;
using Primal.Api;

namespace Primal.Api
{
	public interface IEventBus
	{
		/// <summary>
		/// Post the specified entityEvent.
		/// </summary>
		/// <param name="entityEvent">Entity event.</param>
		void Post(IEntityEvent entityEvent) ;

		/// <summary>
		/// Register the specified handler for a type of entity event.
		/// </summary>
		/// <param name="handler">Handler.</param>
		/// <param name="typeId">Type identifier.</param>
		void Register(IEventHandler handler, int typeId);

		/// <summary>
		/// Deregister the specified handler and typeId.
		/// </summary>
		/// <param name="handler">Handler.</param>
		/// <param name="typeId">Type identifier.</param>
		void Deregister(IEventHandler handler, int typeId);
	}
}

