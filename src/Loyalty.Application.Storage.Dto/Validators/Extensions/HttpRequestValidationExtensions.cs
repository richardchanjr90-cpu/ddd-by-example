using System;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Loyalty.Application.Storage.Dto.Validators.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace Loyalty.Application.Storage.Dto.Validators.Extensions
{
    public static class HttpRequestValidationExtensions
    {
        public static async Task<ValidatableRequest<T>> GetJsonBody<T, V>(this HttpRequest request)
            where V : AbstractValidator<T>, new()
        {
            var requestObject = await request.GetJsonBody<T>();
            var validator = new V();
            var validationResult = validator.Validate(requestObject);

            if (!validationResult.IsValid)
            {
                return new ValidatableRequest<T>
                {
                    Value = requestObject,
                    IsValid = false,
                    Errors = validationResult.Errors
                };
            }

            return new ValidatableRequest<T>
            {
                Value = requestObject,
                IsValid = true
            };
        }

        public static async Task<T> GetJsonBody<T>(this HttpRequest request)
        {
            var requestBody = await request.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(requestBody);
        }
    }
}
