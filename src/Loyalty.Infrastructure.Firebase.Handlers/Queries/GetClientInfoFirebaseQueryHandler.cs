using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken.FunctionBinding.TokenProviders.Firebase;
using FirebaseAdmin.Auth;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Firebase.Queries.Queries;
using Loyalty.Domain.Handlers.Firebase.Queries.QueryResults;
using MediatR;

namespace Loyalty.Infrastructure.Firebase.Handlers.Queries
{
    public class GetClientInfoFirebaseQueryHandler : 
        BaseFirebaseHandler,
        IRequestHandler<GetClientInfoFirebaseQuery, GetClientInfoFirebaseQueryResult>
    {
        private const string VerifyCustomTokenUrl =
            "https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyCustomToken?key=";

        public async Task<GetClientInfoFirebaseQueryResult> Handle(
            GetClientInfoFirebaseQuery request, 
            CancellationToken cancellationToken)
        {
            var token = await GetAuthTokenAsync(request.UserId, request.GoogleAuthKey);
            var decoded = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
            var claims = decoded.Claims.Select(claim => claim.ToClaim()).ToList();
            var identity = new ClaimsIdentity(claims, "Bearer");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var userId = claimsPrincipal.GetUserId();
            var name = claimsPrincipal.GetName();
            var surname = claimsPrincipal.GetSurname();
            var phone = claimsPrincipal.GetPhone();
            var city = claimsPrincipal.GetCity();
            var avatar = claimsPrincipal.GetAvatarOrNull();

            return new GetClientInfoFirebaseQueryResult()
            {
                UserId = userId,
                Name = name,
                City = city,
                Phone = phone,
                PhotoUrl = avatar,
                Surname = surname
            };
        }

        public async Task<string> GetAuthTokenAsync(string uid, string key)
        {
            using (var client2 = new HttpClient())
            {
                await ThrowIfUserDoesNotExist(uid);
                var customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid);

                var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("token", customToken),
                    new KeyValuePair<string, string>("returnSecureToken", true.ToString())
                });

                var response = await client2.PostAsync(new Uri($"{VerifyCustomTokenUrl}{key}"), content);
                var idTokenModel = await response.Content.ReadAsStringAsync();
                var model = (JsonElement) JsonSerializer.Deserialize<object>(idTokenModel);
                return model.GetProperty("idToken").ToString();
            }
        }

        private static async Task ThrowIfUserDoesNotExist(string uid)
        {
            try
            {
                var user = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
            }
            catch (Exception ex)
            {
                throw new LoyaltyValidationException("User does not exist.", ex, ErrorCode.USER_DOES_NOT_EXIST);
            }
        }
    }
}
