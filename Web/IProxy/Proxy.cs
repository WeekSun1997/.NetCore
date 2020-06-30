using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace IProxy
{
    public class Proxy : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            throw new NotImplementedException();
        }
    }
}
