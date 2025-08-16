using AutoMapper;
using Loyalty.Core.ViewModels;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Location;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;

namespace Loyalty.Domain.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LocationViewModel, GetLocationQueryResult>();

            CreateMap<GetVenueByIdQueryResult, VenueViewModel>()
                .ForMember(x=>x.Details, opt=> opt.Ignore());

            CreateMap<VenueViewModel, CreateVenueCommand>()
                .ForMember(x => x.Details, opt => opt.MapFrom(src => src.Details));

            CreateMap<VenueViewModel, UpdateVenueCommand>()
                .ForMember(x => x.Details, opt => opt.MapFrom(src => src.Details));
        }
    }
}
