using AutoMapper;
using Loyalty.Application.ViewModels;
using Loyalty.Application.ViewModels.Location;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;
using Loyalty.Application.ViewModels.LoyaltyProgram;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Application.ViewModels.Purchase;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Domain.Handlers.Queries.Commands.Locations;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.VenueDetails;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.Purchase;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.VenueDetails;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;

namespace Loyalty.Application.AutoMapper
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

            
            CreateMap<PurchaseViewModel, CreatePurchaseCommand>();
            CreateMap<ActivePurchaseResult, ActivePurchaseViewModel>();
            CreateMap<GetActivePurchaseResult, ActivePurchasesViewModel>();

            CreateMap<GetLoyaltyProgramByIdQueryResult, LoyaltyProgramViewModel>();
            CreateMap<LoyaltyProgramViewModel, CreateLoyaltyProgramCommand>();
            CreateMap<LoyaltyProgramViewModel, UpdateLoyaltyProgramCommand>();

            CreateMap<GetProductByIdQueryResult, ProductViewModel>();
            CreateMap<ProductViewModel, CreateProductCommand>();
            CreateMap<ProductViewModel, UpdateProductCommand>();

            CreateMap<GetLoyaltyProductGroupByIdQueryResult, LoyaltyProductGroupViewModel>();
            CreateMap<LoyaltyProductGroupViewModel, CreateLoyaltyProductGroupCommand>();
            CreateMap<LoyaltyProductGroupViewModel, UpdateLoyaltyProductGroupCommand>();

            CreateMap<GetProductGroupByIdQueryResult, ProductGroupViewModel>();
            CreateMap<ProductGroupViewModel, ProductGroupViewModel>();
            CreateMap<ProductGroupViewModel, ProductGroupViewModel>();

            CreateMap<GetWorkerByIdQueryResult, WorkerViewModel>();
            CreateMap<CreateWorkerCommand, WorkerViewModel>();
            CreateMap<UpdateWorkerCommand, WorkerViewModel>();

        }
    }
}
