using System;
using System.Collections.Generic;
using System.Linq;
using Primal.Api;

namespace Primal
{
	/// <summary>
	/// Provides a wrapper around the systems, calls the methods of the system in the correct order. 
	/// Hides system implementation from the API users.
	/// </summary>
	class SystemWrapper
	{
		public AbstractSystem System { get; private set; }

		private IList<IEntity> entities;

		public SystemWrapper(AbstractSystem system)
		{
			System = system;
			entities = new List<IEntity>();
		}

		public void Update(double elapsedMs)
		{
			System.BeforeUpdate(elapsedMs);
			System.Update(entities, elapsedMs);
			System.AfterUpdate(elapsedMs);
		}

		public void AddEntity(IEntity entity)
		{
			if (!entity.ContainsAll(System.RegisteredComponents)) {
				//entity does not have the right components
				return;
			}
			if (entities.Contains(entity)) {
				//system already contains the entity.
				return;
			}
			entities.Add(entity);
			System.EntityAdded(entity);
		}

		public void RemoveEntity(IEntity entity)
		{
			if (!entities.Contains(entity)) {
				//system does not contain the entity.
				return;
			}
			entities.Remove(entity);
			System.EntityRemoved(entity);
		}

		public void UpdateEntityValidity(IEntity entity)
		{
			if (entities.Contains(entity)) {
				if (!entity.ContainsAll(System.RegisteredComponents)) {
					RemoveEntity(entity);
				}
			} else {
				if (entity.ContainsAll(System.RegisteredComponents)) {
					AddEntity(entity);
				}
			}
		}

		public Boolean IsDraw()
		{
			return System.IsDraw();
		}

		public int EntityCount {
			get {
				return entities.Count();
			}
		}
	}
}
