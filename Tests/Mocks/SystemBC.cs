using Primal.Api;

namespace Primal.Tests
{
	class SystemBC : UpdateSystem
	{
		protected internal override void AddKeyComponents()
		{
			AddKeyComponent<ComponentB>();
			AddKeyComponent<ComponentC>();
		}

		protected internal override void UpdateEntity(IEntity entity, double elapsedMs)
		{
		}
	}
}
