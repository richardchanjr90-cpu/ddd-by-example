using System.Collections.Generic;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.UserProfile
{
    public class GetUserProfileByIdQueryResult
    {
        public string Phone { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhotoUri { get; set; }

        public string PositionName { get; set; }
    }
}