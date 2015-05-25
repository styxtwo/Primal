using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    class SystemA :BaseSystem {
        protected internal override void AddKeyComponents() {
            AddKeyComponent<ComponentA>();
        }

        protected internal override void UpdateEntity(Entity entity, double elapsedMs) {
        }
    }
}
