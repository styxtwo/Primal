using Primal.Api;

namespace Primal.Tests
{
	class SystemBC : UpdateSystem
	{
		public SystemBC()
		{
			RegisterComponent<ComponentB>();
			RegisterComponent<ComponentC>();
		}

		protected internal override void UpdateEntity(IEntity entity, double elapsedMs)
		{
		}
	}
}
