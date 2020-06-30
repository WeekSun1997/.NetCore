using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoFacAop
{
    public abstract class CacheBase : IInterceptor
    {
        public abstract void Intercept(IInvocation invocation);
    }
}
