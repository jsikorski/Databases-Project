using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Administration.Infrastucture
{
    public interface IBusyScopeSubscreen
    {
        void SetBusyScope(IBusyScope busyScope);
    }
}
