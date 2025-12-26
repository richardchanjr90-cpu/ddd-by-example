namespace Loyalty.Domain.Handlers.Queries.Commands.Locations
{
    public class CreateLocationCommand
    {
        public string City { get; set; }

        public string Address { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}