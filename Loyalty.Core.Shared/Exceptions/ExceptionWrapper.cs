using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loyalty.Core.Shared.Exceptions
{
    public class ExceptionWrapper
    {
        public static async Task<IActionResult> Handle(Func<Task<IActionResult>> action)
        {
            try
            {
                var result = await action();
                return result;
            }
            catch (HttpResponseException)
            {
                return new UnauthorizedResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
