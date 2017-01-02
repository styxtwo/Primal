using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Primal
{
	/// <summary>
	/// Provides a collection for the systems.
	/// </summary>
	class Systems : IEventHandler
	{
		private IDictionary<Type, SystemWrapper> wrappers;
		EntityFinder entityFinder;

		public Systems(EntityFinder finder, IEventBus eventBus)
		{
			entityFinder = finder;
			wrappers = new Dictionary<Type, SystemWrapper>();
			eventBus.Register(this, EntityEventTypes.EntityAdded);
			eventBus.Register(this, EntityEventTypes.EntityRemoved);
			eventBus.Register(this, EntityEventTypes.EntityChanged);
		}
		//-- Addition methods --//
		public bool Add(AbstractSystem system)
		{
			if (wrappers.ContainsKey(system.GetType())) {
				return false;
			}
			SystemWrapper wrapper = new SystemWrapper(system);
			wrappers.Add(system.GetType(), wrapper);

			IEnumerable<IEntity> existingEntities =
				entityFinder.Find(system.RegisteredComponents);
			foreach (Entity enitity in existingEntities) {
				// Why was this entity instead of Ientity again? was it dispose?
				wrapper.AddEntity(enitity);
			}
			return true;
		}
		//-- Update methods --//
		internal void Update(double elapsedMs)
		{
			Update(elapsedMs, wrappers.Values.Where(system => !system.IsDraw()));
		}

		internal void Draw(double elapsedMs)
		{
			Update(elapsedMs, wrappers.Values.Where(system => system.IsDraw()));
		}

		private void Update(double elapsedMs, IEnumerable<SystemWrapper> systems)
		{
			foreach (SystemWrapper system in systems) {
				system.Update(elapsedMs);
			}
		}
		//-- Entity Added/Removed Methods --//
		public void HandleEvent(IEntityEvent e)
		{
			IEntity entity = e.Source;
			switch (e.Type) {
			case EntityEventTypes.EntityAdded:
				EntityAdded(entity);
				break;
			case EntityEventTypes.EntityRemoved:
				EntityRemoved(entity);
				break;
			case EntityEventTypes.EntityChanged:
				EntityChanged(entity);
				break;
			}
		}

		private void EntityAdded(IEntity entity)
		{
			foreach (SystemWrapper system in wrappers.Values) {
				system.AddEntity(entity);
			}
		}

		private void EntityRemoved(IEntity entity)
		{
			foreach (SystemWrapper system in wrappers.Values) {
				system.RemoveEntity(entity);
			}
		}

		private void EntityChanged(IEntity entity)
		{
			foreach (SystemWrapper system in wrappers.Values) {
				system.UpdateEntityValidity(entity);
			}
		}
		//-- Debug data Methods --// 
		public int GetEntityCount(object system)
		{
			SystemWrapper wrapper;
			wrappers.TryGetValue(system.GetType(), out wrapper);
			if (wrapper == null) {
				throw (new ArgumentException("System not found."));
			}
			return wrapper.EntityCount;
		}

		public int BaseSystemCount {
			get {
				return wrappers.Values.Count(system => !system.IsDraw());
			}
		}

		public int DrawSystemCount {
			get {
				return wrappers.Values.Count(system => system.IsDraw());
			}
		}

		public IEnumerable<Type> BaseSystems()
		{
			return wrappers.Where(pair => !pair.Value.IsDraw()).Select(pair => pair.Key);
		}

		public IEnumerable<Type> DrawSystems()
		{
			return wrappers.Where(pair => pair.Value.IsDraw()).Select(pair => pair.Key);
		}
	}
}
