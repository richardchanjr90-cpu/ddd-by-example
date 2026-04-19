using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken.FunctionBinding.TokenProviders.Firebase;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Firebase.Queries.Queries;
using Loyalty.Domain.Handlers.Firebase.Queries.QueryResults;
using MediatR;
using ErrorCode = Loyalty.Common.Shared.Constants.ErrorCode;

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
            string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(request.JsonInBase64));
            var appOptions = new AppOptions()
            {
                Credential = GoogleCredential.FromJson(credentials)
            };

            var client = FirebaseApp.GetInstance("client") ?? FirebaseApp.Create(appOptions, "client");
            var otherAuth = FirebaseAuth.GetAuth(client);

            var token = await GetAuthTokenAsync(request.UserId, request.GoogleAuthKey, otherAuth);

            var decoded = await otherAuth.VerifyIdTokenAsync(token);
            var claims = decoded.Claims.Select(claim => claim.ToClaim()).ToList();
            var identity = new ClaimsIdentity(claims, "Bearer");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            try
            {
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
            catch (Exception e)
            {
                throw new LoyaltyValidationException("User registration is not complete.", ErrorCode.USER_DOES_NOT_EXIST);
            }
        }

        public async Task<string> GetAuthTokenAsync(string uid, string key, FirebaseAuth auth)
        {
            using var client2 = new HttpClient();
            await ThrowIfUserDoesNotExist(auth, uid);
            var customToken = await auth.CreateCustomTokenAsync(uid);

            var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("token", customToken),
                new KeyValuePair<string, string>("returnSecureToken", true.ToString())
            });

            var response = await client2.PostAsync(new Uri($"{VerifyCustomTokenUrl}{key}"), content);
            var idTokenModel = await response.Content.ReadAsStringAsync();
            var model = (JsonElement)JsonSerializer.Deserialize<object>(idTokenModel);
            return model.GetProperty("idToken").ToString();
        }

        private static async Task ThrowIfUserDoesNotExist(FirebaseAuth auth, string uid)
        {
            try
            {
                var user = await auth.GetUserAsync(uid);
            }
            catch (Exception ex)
            {
                throw new LoyaltyValidationException("User does not exist.", ex, ErrorCode.USER_DOES_NOT_EXIST);
            }
        }
    }
}
