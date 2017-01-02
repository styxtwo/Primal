using Primal.Api;

namespace Primal
{
	/// <summary>
	/// Implementation of the IWorld interface.
	/// </summary>
	class World : IPrimalWorld
	{
		public IDebugInfo DebugInfo { get; private set; }

		Entities entities;
		Systems systems;
		IEventBus eventBus;

		public World()
		{
			eventBus = new EventBus();
			entities = new Entities(eventBus);
			systems = new Systems(entities.EntityFinder, eventBus);
			DebugInfo = new DebugInfo(entities, systems);
		}

		public IPrimalWorld AddSystems(AbstractSystem[] inputSystems)
		{
			foreach (AbstractSystem system in inputSystems) {
				AddSystem(system);
			}
			return this;
		}

		public IPrimalWorld AddSystem(AbstractSystem system)
		{
			systems.Add(system);
			return this;
		}

		public IEntity CreateEntity()
		{
			Entity entity = new Entity();
			entities.Add(entity);
			return entity;
		}

		public void RemoveEntity(IEntity entity)
		{
			if (entity is Entity) {
				Entity converted = (entity as Entity);
				converted.DisposeEntity();
				entities.Remove(converted);
			}
		}

		public void Update(double elapsedMs)
		{
			systems.Update(elapsedMs);
		}

		public void Draw(double elapsedMs)
		{
			systems.Draw(elapsedMs);
		}

		public IFinder EntityFinder {
			get {
				return entities.EntityFinder;
			}
		}
	}
}
