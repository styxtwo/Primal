using Primal.Api;

namespace Primal.Tests
{
	class SystemB : UpdateSystem
	{
		public SystemB()
		{
			RegisterComponent<ComponentB>();
		}

		protected internal override void UpdateEntity(IEntity entity, double elapsedMs)
		{
		}
	}
}
