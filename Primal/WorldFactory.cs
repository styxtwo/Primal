using Primal.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal {
    public class WorldFactory {
        public static IWorld Create() {
            return new World();
        }
    }
}
