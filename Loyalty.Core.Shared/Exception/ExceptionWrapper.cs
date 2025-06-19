using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loyalty.Core.Shared.Exception
{
    public class ExceptionWrapper
    {
        public static async Task<IActionResult> Handle(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (HttpResponseException ex)
            {
                return new BadRequestObjectResult(ex.Response);
            }
        }
    }
}
