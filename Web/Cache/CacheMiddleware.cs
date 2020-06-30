using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Cache
{
    public class CacheMiddleware
    {

        public readonly RequestDelegate _Delegate;
        public CacheMiddleware(RequestDelegate @delegate)
        {

            this._Delegate = @delegate;
        }
        public Task Invoke(HttpContext Invoke)
        {


            return _Delegate.Invoke(Invoke);
        }
    }
}
