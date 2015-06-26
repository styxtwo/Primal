using Primal.Api;
using System;
using System.Collections.Generic;

namespace Primal {

    /// <summary>
    /// Provides a system that contains the logic of the world.
    /// Systems (only) act on entities with the correct components.
    /// </summary>
    public abstract class BaseSystem {

        /// <summary>
        /// Reference to the world this system resides in.
        /// </summary>
        protected internal IWorld World { get; set; }

        /// <summary>
        /// Collection of the components an entity needs for the system to act on it.
        /// </summary>
        internal ICollection<Type> KeyComponents { get; set; }

        /// <summary>
        /// Constructor of the base system.
        /// </summary>
        public BaseSystem() {
            KeyComponents = new HashSet<Type>();
            AddKeyComponents();
        }

        /// <summary>
        /// Adds a component to the list of key components for the system.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected internal void AddKeyComponent<T>() where T : Component {
            KeyComponents.Add(typeof(T));
        }

        /// <summary>
        /// Abstract method that needs to be implemented by the child classes, giving them an 
        /// opportunity to add the key components.
        /// </summary>
        protected internal abstract void AddKeyComponents();

        /// <summary>
        /// An update for a specific entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="elapsedMs">he elapsed time since the last update.</param>
        protected internal abstract void UpdateEntity(Entity entity, double elapsedMs);

        /// <summary>
        /// Before a specific update.
        /// </summary>
        /// <param name="elapsedMs">The elapsed time since the last update.</param>
        protected internal virtual void BeforeUpdate(double elapsedMs) {
            //Empty, to be overridden.
        }

        /// <summary>
        /// After a specific update.
        /// </summary>
        /// <param name="elapsedMs">The elapsed time since the last update.</param>
        protected internal virtual void AfterUpdate(double elapsedMs) {
            //Empty, to be overridden.
        }

        /// <summary>
        /// Methods that gets called when the system adds an entity.
        /// </summary>
        /// <param name="entity"> The entity that got added. </param>
        protected internal virtual void EntityAdded(Entity entity) {
            //Empty, to be overridden.
        }

        /// <summary>
        /// Method that gets called when the system removes an entity.
        /// </summary>
        /// <param name="entity">The entity that got removed.</param>
        protected internal virtual void EntityRemoved(Entity entity) {
            //Empty, to be overridden.
        }
    }
}
