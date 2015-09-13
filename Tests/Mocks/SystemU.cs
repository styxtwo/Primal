using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    class SystemU : BaseSystem {
        public Boolean IsUpdated { get; set; }

        protected internal override void BeforeUpdate(double elapsedMs) {
            IsUpdated = true;
        }

        protected internal override void UpdateEntity(Entity entity, double elapsedMs) {
        }

        protected internal override void AddKeyComponents() {
        }
    }
}
