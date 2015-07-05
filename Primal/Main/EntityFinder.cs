using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Main {
    /// <summary>
    /// Implementation of the IFinder interface.
    /// </summary>
    class EntityFinder : IFinder {
        private ISet<Entity> entities;


        public EntityFinder(ISet<Entity> entities) {
            this.entities = entities;
        }

        public IEnumerable<Entity> Find(params Type[] components) {
            IList<Entity> selected = new List<Entity>();
            foreach (Entity entity in entities) {
                if (entity.ContainsAll(components)) {
                    selected.Add(entity);
                }
            }
            return selected;
        }

        public IEnumerable<Entity> Find(IEnumerable<Type> components) {
            return Find(components.ToArray());
        }

        public IEnumerable<Entity> Find<T>() {
            return Find(typeof(T));
        }

        public IEnumerable<Entity> Find<T, U>() {
            return Find(typeof(T), typeof(U));
        }

        public IEnumerable<Entity> Find<T, U, V>() {
            return Find(typeof(T), typeof(U), typeof(V));
        }

        public Entity FindFirst(params Type[] components) {
            return Find(components).FirstOrDefault();
        }

        public Entity FindFirst<T>() {
            return FindFirst(typeof(T));
        }

        public Entity FindFirst<T, U>() {
            return FindFirst(typeof(T), typeof(U));
        }

        public Entity FindFirst<T, U, V>() {
            return FindFirst(typeof(T), typeof(U), typeof(V));
        }
    }
}
