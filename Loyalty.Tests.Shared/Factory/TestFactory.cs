using System.Collections.Generic;
using System.IO;
using System.Text;
using Loyalty.Tests.Shared.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Loyalty.Tests.Shared.Factory
{
    public class TestFactory
    {
        public static IEnumerable<object[]> Data()
        {
            return new List<object[]>
            {
                new object[] {"name", "Bill"},
                new object[] {"name", "Paul"},
                new object[] {"name", "Steve"}
            };
        }

        private static Dictionary<string, StringValues> CreateDictionary(string key, string value)
        {
            var qs = new Dictionary<string, StringValues>
            {
                {key, value}
            };
            return qs;
        }

        public static DefaultHttpRequest CreateHttpRequest(string queryStringKey, string queryStringValue)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Query = new QueryCollection(CreateDictionary(queryStringKey, queryStringValue))
            };
            return request;
        }

        public static DefaultHttpRequest CreateHttpRequest<T>(T body)
        {
            var bodyString = JsonConvert.SerializeObject(body);
            var buffer = Encoding.UTF8.GetBytes(bodyString);

            using (var memory = new MemoryStream())
            {
                var request = new DefaultHttpRequest(new DefaultHttpContext())
                {
                    Body = memory,
                    ContentType = "application/json"
                };

                return request;
            }
        }

        public static ILogger CreateLogger(LoggerTypes type = LoggerTypes.Null)
        {
            ILogger logger;

            if (type == LoggerTypes.List)
                logger = new ListLogger();
            else
                logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");

            return logger;
        }
    }
}