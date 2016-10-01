using Primal.Api;

namespace Primal.Tests
{
	class SystemA : UpdateSystem
	{
		protected internal override void AddKeyComponents()
		{
			AddKeyComponent<ComponentA>();
		}

		protected internal override void UpdateEntity(IEntity entity, double elapsedMs)
		{
		}
	}
}
