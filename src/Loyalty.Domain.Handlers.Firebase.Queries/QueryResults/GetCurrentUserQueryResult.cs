namespace Loyalty.Domain.Handlers.Firebase.Queries.QueryResults
{
    public class GetCurrentUserQueryResult
    {
        public string Email { get; set; }

        public string UserId { get; set; }

        public bool IsEmailVerified { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
    }
}