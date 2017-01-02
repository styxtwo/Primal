using System;

namespace Primal.Api
{
	public interface IEntityEvent
	{
		IEntity Source { get; }

		int Type { get; }

		bool IsType(int type);
	}
}

