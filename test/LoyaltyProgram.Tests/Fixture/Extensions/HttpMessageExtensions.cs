using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LoyaltyProgram.Tests.Fixture.Extensions
{
    public static class HttpMessageExtensions
    {
        public static async Task<T> DeserializeAsync<T>(this HttpResponseMessage message)
        {
            var resultString = await message.Content.ReadAsStringAsync();
            var myContent = JsonConvert.DeserializeObject<T>(resultString);
            return myContent;
        }
    }
}
