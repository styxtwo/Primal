using Primal.Api;
using System;

namespace Primal.Tests
{
	/// <summary>
	/// Provides a system which is a draw system, and thus only updated when the draw method is called on world.
	/// </summary>
	class SystemD : DrawSystem
	{
		public Boolean IsUpdated { get; set; }

		public SystemD()
		{
			RegisterComponent<ComponentD>();
		}

		protected internal override void BeforeUpdate(double elapsedMs)
		{
			IsUpdated = true;
		}

		protected internal override void UpdateEntity(IEntity entity, double elapsedMs)
		{
		}
	}
}
