using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ip2.Utils
{
    public class CustomPageFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var ClientIPAddr = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4();
            var result = context.Result;
            if (result is PageResult)
            {
                var page = ((PageResult)result);
                page.ViewData["IP"] = ClientIPAddr.ToString();
            }
            await next.Invoke();
        }
    }
}
