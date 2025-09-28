using AutoMapper;
using Loyalty.Application.ViewModels.Location;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;
using Loyalty.Application.ViewModels.LoyaltyProgram;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Application.ViewModels.Purchase;
using Loyalty.Application.ViewModels.Rule;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Domain.Handlers.Queries.Commands.Locations;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using Loyalty.Domain.Handlers.Queries.Commands.Rules;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.VenueDetails;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.Purchase;
using Loyalty.Domain.Handlers.Queries.QueryResults.Rules;
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
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate());
            
            CreateMap<LocationViewModel, UpdateLocationCommand>();

            CreateMap<GetVenueByIdQueryResult, VenueViewModel>();

            CreateMap<VenueViewModel, CreateVenueCommand>()
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.IsPublished, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.IsApproved, opt => opt.DoNotValidate());

            CreateMap<VenueViewModel, UpdateVenueCommand>()
                .ForSourceMember(x => x.IsPublished, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.IsApproved, opt => opt.DoNotValidate());

            CreateMap<GetVenueDetailsByIdQueryResult, VenueDetailsViewModel>();

            CreateMap<VenueDetailsViewModel, CreateVenueDetailsCommand>()
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate())
                .ForMember(x => x.VenueId, opt => opt.Ignore());

            CreateMap<VenueDetailsViewModel, UpdateVenueDetailsCommand>()
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate())
                .ForMember(x => x.VenueId, opt => opt.Ignore());

            CreateMap<GetVenueFullByIdQueryResult, VenueFullViewModel>();

            CreateMap<ActivePurchaseResult, ActivePurchaseViewModel>();
            CreateMap<GetActivePurchaseResult, ActivePurchasesViewModel>();

            CreateMap<GetLoyaltyProgramByIdQueryResult, LoyaltyProgramViewModel>();
            CreateMap<LoyaltyProgramViewModel, CreateLoyaltyProgramCommand>()
                .ForMember(x => x.UserId, opt => opt.Ignore())
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate());
            CreateMap<LoyaltyProgramViewModel, UpdateLoyaltyProgramCommand>()
                .ForMember(x => x.UserId, opt => opt.Ignore());

            CreateMap<GetProductByIdQueryResult, ProductViewModel>();
            CreateMap<ProductViewModel, CreateProductCommand>()
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate());
            CreateMap<ProductViewModel, UpdateProductCommand>();

            CreateMap<GetLoyaltyProductGroupByIdQueryResult, LoyaltyProductGroupGetViewModel>();

            CreateMap<GetProductGroupByIdQueryResult, ProductGroupViewModel>();
            CreateMap<ProductGroupViewModel, CreateProductGroupCommand>()
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate());
            CreateMap<ProductGroupViewModel, UpdateProductGroupCommand>();

            CreateMap<GetWorkerByIdQueryResult, WorkerViewModel>();
            CreateMap<WorkerViewModel, CreateWorkerCommand>()
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate())
                .ForMember(x => x.VenueId, opt => opt.Ignore());
            CreateMap<WorkerViewModel, UpdateWorkerCommand>()
                .ForMember(x => x.VenueId, opt => opt.Ignore());

            CreateMap<GetRuleByIdQueryResult, RuleViewModel>();
            CreateMap<GetSingleRuleByIdQueryResult, SingleRuleViewModel>();
        }
    }
}
