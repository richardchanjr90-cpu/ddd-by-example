using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loyalty.Common.Shared.Exceptions
{
    public class ExceptionWrapper
    {
        //This one does not work in AF2.0 right now. Currently, needs to be wrapped like so.
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