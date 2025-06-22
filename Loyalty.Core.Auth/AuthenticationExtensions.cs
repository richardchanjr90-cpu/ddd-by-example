using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Loyalty.Core.Auth.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Refit;

namespace Loyalty.Core.Auth
{
    public static class AuthenticationExtensions
    {
        public static readonly HttpClientHandler CachedHandler = new HttpClientHandler();

        /// <summary>
        /// ClaimsPrincipal.Current is currently always null in Functions v2 on dotnet core. 
        /// using this to solve the problem as a temp solution, should be fixed by the end of 2018
        /// https://github.com/Azure/azure-functions-host/issues/33
        /// </summary>
        public static bool TryAuthenticate(this HttpRequest request, out AuthenticationModel model)
        {
            bool isAuthenticated = false;
            model = null;

            try
            {
                var client = new HttpClient(CachedHandler)
                {
                    BaseAddress = new Uri(Environment.GetEnvironmentVariable("AuthenticationBaseAddress")
                                          ?? new Uri(request.GetDisplayUrl()).GetLeftPart(UriPartial.Authority))
                };

                var header = request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer", string.Empty).Trim();


                if (!String.IsNullOrEmpty(header))
                {
                    //AddTokenToAuthSession(client, request, header);
                    TokenValidator.Validate(header);
                }
                else if (!String.IsNullOrEmpty(request.Cookies["AppServiceAuthSession"]))
                {
                    AddTokenToAuthSession(client, request, request.Cookies["AppServiceAuthSession"]);
                }
                else
                {
                    //dev
                    AddTokenToAuthSession(client, request, Environment.GetEnvironmentVariable("AuthenticationToken"));
                }

                var service = RestService.For<IAuthentication>(client);
                model = service.GetCurrentAuthentication().Result.SingleOrDefault();
                isAuthenticated = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return isAuthenticated;
        }

        private static void AddTokenToAuthSession(HttpClient client, HttpRequest request, string token)
        {
            CachedHandler.CookieContainer.Add(client.BaseAddress,
                new Cookie("AppServiceAuthSession", token));
        }
    }
}
