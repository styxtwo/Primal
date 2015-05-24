using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.API {
    public interface IWorld {
        void AddSystem(Process system);
        void AddEntity(IEntity entity);
        
        void RemoveEntity(IEntity entity);
        void Update(double elapsedMs);
    }
}
