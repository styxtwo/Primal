using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Primal
{
	/// <summary>
	/// Provides a collection for the systems.
	/// </summary>
	class Systems
	{
		private IDictionary<Type, SystemWrapper> wrappers;

		public Systems(Entities entities)
		{
			wrappers = new Dictionary<Type, SystemWrapper>();
			entities.EntityAdded += EntityAdded;
			entities.EntityRemoved += EntityRemoved;
			entities.EntityChanged += EntityChanged;
		}
		//-- Addition methods --//
		public bool Add(AbstractSystem system, IEnumerable<IEntity> existingEntities)
		{
			if (wrappers.ContainsKey(system.GetType())) {
				return false;
			}
			SystemWrapper wrapper = new SystemWrapper(system);
			wrappers.Add(system.GetType(), wrapper);

			foreach (Entity enitity in existingEntities) {
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
		private void EntityAdded(Entity entity)
		{
			foreach (SystemWrapper system in wrappers.Values) {
				system.AddEntity(entity);
			}
		}

		private void EntityRemoved(Entity entity)
		{
			foreach (SystemWrapper system in wrappers.Values) {
				system.RemoveEntity(entity);
			}
		}

		private void EntityChanged(Entity entity)
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
