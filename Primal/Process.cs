using Primal.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal {
    public abstract class Process {
        private IList<IEntity> entities;
        private ISet<Type> keyComponents;

        public Process() {
            entities = new List<IEntity>();
            keyComponents = new HashSet<Type>();
            AddKeyComponents();
        }

        protected bool AddComponent<K>() where K : IComponent {
            return keyComponents.Add(typeof(K));
        }

        internal void Update(double elapsedMs) {
            BeforeUpdate();
            foreach (IEntity entity in entities.ToList()) {
                UpdateEntity(entity);
            }
            AfterUpdate();
        }

        internal bool AddEntity(IEntity entity) {
            if (!entity.ContainsAll(keyComponents)) {
                //entity does not have the right components
                return false;
            }
            if(entities.Contains(entity)) {
                //system already contains the entity.
                return false;
            }
            entities.Add(entity);
            EntityAdded(entity);
            return true;
        }

        internal void RemoveEntity(IEntity entity) {
            if (!entities.Contains(entity)) {
                //system does not contain the entity.
                return;
            }
            entities.Remove(entity);
            EntityRemoved(entity);
        }

        internal void ChangeEntity(IEntity entity) {
            if (entities.Contains(entity)) {
                if (!entity.ContainsAll(keyComponents)) {
                    RemoveEntity(entity);
                }
            }
            else {
                if (entity.ContainsAll(keyComponents)){
                    AddEntity(entity);
                }
            }
        }


        protected abstract void AddKeyComponents();

        protected abstract void UpdateEntity(IEntity entity);

        protected virtual void BeforeUpdate() {
            //Empty, to be overridden in child classes.
        }

        protected virtual void AfterUpdate() {
            //Empty, to be overridden in child classes.
        }

        protected virtual void EntityAdded(IEntity entity) {
            //Empty, to be overridden in child classes.
        }

        protected virtual void EntityRemoved(IEntity entity) {
            //Empty, to be overridden in child classes.
        }
    }
}
