using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Domain.Handlers.Firebase.Queries.QueryResults
{
    public class GetClientInfoFirebaseQueryResult
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string PhotoUrl { get; set; }
    }
}
