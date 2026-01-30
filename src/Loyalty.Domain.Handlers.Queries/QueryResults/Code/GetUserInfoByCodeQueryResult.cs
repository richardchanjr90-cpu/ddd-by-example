using System.Text.Json.Serialization;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Code
{
    public class GetUserInfoByCodeQueryResult
    {
        public string UserId { get; set; }
    }
}