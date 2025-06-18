using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Loyalty.Data.Entities
{
    public class Card
    {
        [BsonElement("loyaltyProgramId")]
        public int LoyaltyProgramId { get; set; }

        [BsonElement("ownerId")]
        public Guid OwnerId { get; set; }
    }
}