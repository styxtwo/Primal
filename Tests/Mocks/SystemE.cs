using Primal.Api;
using System;

namespace Primal.Tests
{
	/// <summary>
	/// Provides a system for which no key components are nessesary
	/// </summary>
	class SystemE : UpdateSystem
	{
		public Boolean IsUpdated { get; set; }

		protected internal override void BeforeUpdate(double elapsedMs)
		{
			IsUpdated = true;
		}

		protected internal override void UpdateEntity(IEntity entity, double elapsedMs)
		{
		}

		protected internal override void AddKeyComponents()
		{
		}
	}
}
