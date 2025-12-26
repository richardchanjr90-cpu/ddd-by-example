using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using FluentValidation;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Loyalty.Infrastructure.IoC
{
    public class HandlerWrapper
    {
        /// <summary>
        /// Catches AuthenticationException and returns UnauthorizedResult, otherwise BadRequestObjectResult. 
        /// </summary>
        public static async Task<IActionResult> WrapAsync(FunctionTokenResult token, Func<Task<IActionResult>> action)
        {
            try
            {
                token.ValidateThrow();
                var result = await action();
                return result;
            }
            catch (AuthenticationException)
            {
                return new UnauthorizedResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public static async Task<IActionResult> WrapAsync(ILogger logger, FunctionTokenResult token, Func<Task<IActionResult>> action)
        {
            try
            {
                token.ValidateThrow();
                var result = await action();
                return result;
            }
            catch (LoyaltyValidationException ex)
            {
                logger?.LogWarning(ex.Message, ex);
                return new BadRequestObjectResult(new CommandResult
                {
                    Success = false,
                    Message = ex.Message,
                    Result = ex.Code ?? new List<string>(0)
                });
            }
            catch (ValidationException ex)
            {
                logger?.LogWarning(ex.Message, ex);
                return new BadRequestObjectResult(new CommandResult
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (AuthenticationException ex)
            {
                logger?.LogWarning(ex.Message, ex);
                return new UnauthorizedResult();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message, ex);
                return new BadRequestObjectResult(new CommandResult
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// Catches AuthenticationException and returns UnauthorizedResult, otherwise BadRequestObjectResult. 
        /// </summary>
        public static IActionResult Wrap(FunctionTokenResult token, Func<IActionResult> action)
        {
            try
            {
                token.ValidateThrow();
                var result = action();
                return result;
            }
            catch (AuthenticationException)
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
