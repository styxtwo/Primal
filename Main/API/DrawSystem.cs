using Primal.Api;
using System;
using System.Collections.Generic;

namespace Primal.Api {

    /// <summary>
    /// Provides a system that contains the logic used in for the drawing of the entities. 
    /// Seperated from the base systems to allow both the update and draw systems to be updated seperately.
    /// </summary>
    public abstract class DrawSystem : AbstractSystem {
        protected internal override Boolean IsDraw() {
            return true;
        }
    }
}
