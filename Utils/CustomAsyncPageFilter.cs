using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ip2.Utils
{
    public class CustomAsyncPageFilter : IAsyncPageFilter
    {
        private readonly IConfiguration _config;
        public string ClientIPAddr { get; private set; }

        public CustomAsyncPageFilter(IConfiguration config)
        {
            _config = config;
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            ClientIPAddr = context.HttpContext.Connection.RemoteIpAddress.ToString();
            
            return Task.CompletedTask;
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
                                                      PageHandlerExecutionDelegate next)
        {
            var result = context.Result;
            if (result is PageResult)
            {
                var page = ((PageResult)result);
                page.ViewData["IP"] = "siema";
            }

            await next.Invoke();
        }
    }
}
