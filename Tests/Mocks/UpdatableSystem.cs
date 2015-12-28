using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    class UpdatableSystem : UpdateSystem {
        public int UpdateEntityCalled { get; private set; }
        public int BeforeUpdateCalled { get; private set; }
        public int AfterUpdateCalled { get; private set; }
        public int EntityAddedCalled { get; private set; }
        public int EntityRemovedCalled { get; private set; }
        protected internal override void AddKeyComponents() {
        }

        protected internal override void UpdateEntity(IEntity entity, double elapsedMs) {
            UpdateEntityCalled++;
        }

        protected internal override void BeforeUpdate(double elapsedMs) {
            BeforeUpdateCalled++;
        }

        protected internal override void AfterUpdate(double elapsedMs) {
            AfterUpdateCalled++;
        }

        protected internal override void EntityAdded(IEntity entity) {
            EntityAddedCalled++;
        }

        protected internal override void EntityRemoved(IEntity entity) {
            EntityRemovedCalled++;
        }
    }
}
