using Primal.Api;

namespace Primal.Tests
{
	class SystemB : UpdateSystem
	{
		protected internal override void AddKeyComponents()
		{
			AddKeyComponent<ComponentB>();
		}

		protected internal override void UpdateEntity(IEntity entity, double elapsedMs)
		{
		}
	}
}
