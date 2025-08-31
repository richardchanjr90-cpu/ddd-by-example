using AutoMapper;
using Loyalty.Core.ViewModels;
using Loyalty.Domain.Handlers.Queries.Commands.Location;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.VenueDetails;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.VenueDetails;

namespace Loyalty.Domain.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LocationViewModel, CreateLocationCommand>()
                .ForSourceMember(x => x.Id, opt => opt.Ignore());
            
            CreateMap<LocationViewModel, UpdateLocationCommand>();

            CreateMap<GetVenueByIdQueryResult, VenueViewModel>();

            CreateMap<VenueViewModel, CreateVenueCommand>()
                .ForSourceMember(x => x.Id, opt => opt.Ignore())
                .ForSourceMember(x => x.IsPublished, opt => opt.Ignore())
                .ForSourceMember(x => x.IsApproved, opt => opt.Ignore());

            CreateMap<VenueViewModel, UpdateVenueCommand>()
                .ForSourceMember(x => x.IsPublished, opt => opt.Ignore())
                .ForSourceMember(x => x.IsApproved, opt => opt.Ignore());

            CreateMap<GetVenueDetailsByIdQueryResult, VenueDetailsViewModel>();

            CreateMap<VenueDetailsViewModel, CreateVenueDetailsCommand>()
                .ForSourceMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.VenueId, opt => opt.Ignore());

            CreateMap<VenueDetailsViewModel, UpdateVenueDetailsCommand>()
                .ForSourceMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.VenueId, opt => opt.Ignore());

            CreateMap<GetVenueFullByIdQueryResult, VenueFullViewModel>();
        }
    }
}
