using System;

namespace Loyalty.Data.Entities
{
    public class Card
    {
        public int LoyaltyProgramId { get; set; }

        public Guid UserId { get; set; }
    }
}