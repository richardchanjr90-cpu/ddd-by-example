using AutoMapper;
using Loyalty.Core.ViewModels;
using Loyalty.Domain.Handlers.Queries.Commands.Location;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Location;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;

namespace Loyalty.Domain.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LocationViewModel, CreateLocationCommand>()
                .ForSourceMember(x => x.Id, opt => opt.Ignore());
            
            CreateMap<LocationViewModel, UpdateLocationCommand>();

            CreateMap<GetVenueByIdQueryResult, VenueViewModel>()
                .ForMember(x=>x.Details, opt=> opt.Ignore());

            CreateMap<VenueViewModel, CreateVenueCommand>()
                .ForMember(x => x.Details, opt => opt.MapFrom(src => src.Details))
                .ForSourceMember(x => x.Id, opt => opt.Ignore())
                .ForSourceMember(x => x.IsPublished, opt => opt.Ignore())
                .ForSourceMember(x => x.IsApproved, opt => opt.Ignore());

            CreateMap<VenueViewModel, UpdateVenueCommand>()
                .ForMember(x => x.Details, opt => opt.MapFrom(src => src.Details))
                .ForSourceMember(x => x.IsPublished, opt => opt.Ignore())
                .ForSourceMember(x => x.IsApproved, opt => opt.Ignore());
        }
    }
}
