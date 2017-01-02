using Primal.Api;

namespace Primal.Tests
{
	class SystemA : UpdateSystem
	{
		public SystemA()
		{
			RegisterComponent<ComponentA>();
		}

		protected internal override void UpdateEntity(IEntity entity, double elapsedMs)
		{
		}
	}
}
