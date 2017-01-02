using Primal.Api;
using System;
using System.Collections.Generic;
using Utilities.Extensions;

namespace Primal
{
	/// <summary>
	/// Provides a collection for the entities.
	/// </summary>
	class Entities
	{
		public EntityFinder EntityFinder { get; private set; }

		private ISet<Entity> entities;
		private IEventBus eventBus;

		public Entities(IEventBus eventBus)
		{
			this.eventBus = eventBus;
			entities = new HashSet<Entity>();
			EntityFinder = new EntityFinder(entities);
		}

		public void Add(Entity entity)
		{
			if (entities.Add(entity)) {
				entity.ComponentsChanged += EntityComponentsChanged;
				eventBus.Post(EntityEvent.Create(entity, EntityEventTypes.EntityAdded));
			}
		}

		void EntityComponentsChanged(IEntity entity)
		{
			eventBus.Post(EntityEvent.Create(entity, EntityEventTypes.EntityChanged));
		}

		public bool Remove(Entity entity)
		{
			if (entities.Remove(entity)) {
				entity.ComponentsChanged -= EntityComponentsChanged;
				eventBus.Post(EntityEvent.Create(entity, EntityEventTypes.EntityRemoved));
				return true;
			}
			return false;
		}

		public int EntityCount {
			get {
				return entities.Count;
			}
		}

		public int ComponentCount {
			get {
				int count = 0;
				foreach (Entity entity in entities) {
					count += entity.ComponentCount;
				}
				return count;
			}
		}
	}
}
