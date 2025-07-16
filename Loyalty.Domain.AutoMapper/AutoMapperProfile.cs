using AutoMapper;
using Loyalty.Core.ViewModels;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.GeoPosition;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;

namespace Loyalty.Domain.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GeoPositionViewModel, GetGeoPositionQueryResult>();
            CreateMap<GetVenueByIdQueryResult, VenueViewModel>();

            CreateMap<VenueViewModel, CreateVenueCommand>();
            CreateMap<VenueViewModel, UpdateVenueCommand>();
        }
    }
}
