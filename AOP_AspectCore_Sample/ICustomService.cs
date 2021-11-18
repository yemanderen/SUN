using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOP_AspectCore_Sample
{
    public interface ICustomService
    {
        [CustomInterceptor]
        void Call();
    }
}
