using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LoyaltyProgram.Tests.Fixture
{
    public class ModelHelper
    {
        public static ByteArrayContent Convert(object data)
        {
            var myContent = JsonConvert.SerializeObject(data);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
        }

        public static async Task<T> DeserializeAsync<T>(HttpResponseMessage message)
        {
            var resultString = await message.Content.ReadAsStringAsync();
            var myContent = JsonConvert.DeserializeObject<T>(resultString);
            return myContent;
        }

        public static T Deserialize<T>(string data)
        {
            var myContent = JsonConvert.DeserializeObject<T>(data);
            return myContent;
        }
    }
}
