using Primal.Api;
using System;
using System.Collections.Generic;

namespace Primal
{
	//TODO Does system do too much?
	/// <summary>
	/// Provides a system that contains the logic of the world.
	/// Systems (only) act on entities with the correct components.
	/// </summary>
	public abstract class AbstractSystem : IEventHandler
	{

		/// <summary>
		/// Reference the entity finder of the world.
		/// </summary>
		protected internal IFinder Finder { protected get; set; }

		/// <summary>
		/// Reference the EventManager, used to post events and register to them.
		/// </summary>
		/// <value>The event manager.</value>
		protected internal IEventBus EventManager { get; set; }

		/// <summary>
		/// Collection of the components an entity needs for the system to act on it.
		/// </summary>
		internal ICollection<Type> RegisteredComponents { get; set; }

		/// <summary>
		/// Constructor of the base system.
		/// </summary>
		internal AbstractSystem()
		{
			RegisteredComponents = new HashSet<Type>();
		}

		/// <summary>
		/// Adds a component to the list of key components for the system.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		protected internal void RegisterComponent<T>() where T : IComponent
		{
			RegisteredComponents.Add(typeof(T));
		}

		/// <summary>
		/// The update methods for the system can be overridden seperately, to allow for different behaviour.
		/// </summary>
		/// <param name="entities">the list of entities</param>
		/// <param name="elapsedMs">the elapsed time</param>
		protected internal virtual void Update(IEnumerable<IEntity> entities, double elapsedMs)
		{
			foreach (Entity entity in entities) {
				UpdateEntity(entity, elapsedMs);
			}
		}

		/// <summary>
		/// Determines whether a system is a draw system or not.
		/// </summary>
		protected internal abstract Boolean IsDraw();

		/// <summary>
		/// An update for a specific entity.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		/// <param name="elapsedMs">he elapsed time since the last update.</param>
		protected internal virtual void UpdateEntity(IEntity entity, double elapsedMs)
		{
			//Empty, to be overridden.
		}

		/// <summary>
		/// Handles events received.
		/// </summary>
		public virtual void HandleEvent(IEntityEvent e)
		{
			//Empty, to be overridden.
		}

		/// <summary>
		/// Before a specific update.
		/// </summary>
		/// <param name="elapsedMs">The elapsed time since the last update.</param>
		protected internal virtual void BeforeUpdate(double elapsedMs)
		{
			//Empty, to be overridden.
		}

		/// <summary>
		/// After a specific update.
		/// </summary>
		/// <param name="elapsedMs">The elapsed time since the last update.</param>
		protected internal virtual void AfterUpdate(double elapsedMs)
		{
			//Empty, to be overridden.
		}

		/// <summary>
		/// Methods that gets called when the system adds an entity.
		/// </summary>
		/// <param name="entity"> The entity that got added. </param>
		protected internal virtual void EntityAdded(IEntity entity)
		{
			//Empty, to be overridden.
		}

		/// <summary>
		/// Method that gets called when the system removes an entity.
		/// </summary>
		/// <param name="entity">The entity that got removed.</param>
		protected internal virtual void EntityRemoved(IEntity entity)
		{
			//#TODO Could be handled as event?
			//Empty, to be overridden.
		}
	}
}
