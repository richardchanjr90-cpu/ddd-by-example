using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;

namespace LoyaltyProgram.Tests.Setup.Auth
{
    public class AuthUser : IDisposable
    {
        public const string DisplayName = "ZalikTest";
        public const string PhonePrefix = "+376";

        private string phone;
        private string uid;

        private readonly UserRecord user;

        private bool IsActive { get; set; } = true;

        public string Phone
        {
            get => IsActive ? phone : throw new ObjectDisposedException("user");
            private set => phone = value;
        }

        public string Uid
        {
            get => IsActive ? uid : throw new ObjectDisposedException("user");
            private set => uid = value;
        }

        public AuthUser(string phone)
        {
            using (HttpClient client = new HttpClient())
            {
                CreateFirebaseInstanceAsync(client).GetAwaiter().GetResult();
            }

            user = CreateFireBaseUser(phone);
            Phone = user.PhoneNumber;
            Uid = user.Uid;
        }

        public AuthUser() 
            : this(null)
        {
        }

        public async Task<string> GetAuthTokenAsync()
        {
            using (var client2 = new HttpClient())
            {
                var customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(Uid);

                var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("token", customToken),
                    new KeyValuePair<string, string>("returnSecureToken", true.ToString())
                });

                var response = await client2.PostAsync(new Uri("https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyCustomToken?key=REDACTED_GOOGLE_KEY"), content);
                var idTokenModel = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject(idTokenModel);
                return ((dynamic)model).idToken.ToString();
            }
        }

        private async Task CreateFirebaseInstanceAsync(HttpClient client)
        {
            var uri = new Uri(
                "https://secretstorage.blob.core.windows.net/firebase/zalik-243111-firebase-adminsdk-83897-987d10f2db.json?sp=r&st=2019-08-17T10:10:57Z&se=2099-08-17T18:10:57Z&spr=https&sv=2018-03-28&sig=REDACTED_SAS_SIG&sr=b");

            using (var result = await client.GetAsync(uri))
            {
                if (result.IsSuccessStatusCode)
                {
                    var stream = await result.Content.ReadAsStreamAsync();

                    if (FirebaseApp.DefaultInstance == null)
                    {
                        FirebaseApp.Create(new AppOptions()
                        {
                            Credential = GoogleCredential.FromStream(stream),
                        });
                    }
                }
            }
        }

        private static UserRecord CreateFireBaseUser(string phone = null)
        {
            var faker = new Faker();
            var args = new UserRecordArgs
            {
                PhoneNumber = phone ?? "+" + faker.Phone.PhoneNumber(PhonePrefix + "29#######"),
                DisplayName = DisplayName
            };

            var userRecord =
                 FirebaseAuth.DefaultInstance.CreateUserAsync(args).GetAwaiter().GetResult();

            return userRecord;
        }

        public void Dispose()
        {
            if (user != null)
            {
                FirebaseAuth.DefaultInstance.DeleteUserAsync(user.Uid).GetAwaiter().GetResult();
                IsActive = false;
                Uid = null;
                Phone = null;
            }
        }
    }
}
