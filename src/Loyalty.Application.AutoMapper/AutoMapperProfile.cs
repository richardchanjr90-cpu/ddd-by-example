using System;
using AutoMapper;
using Loyalty.Application.ViewModels.ClientInfo;
using Loyalty.Application.ViewModels.Location;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;
using Loyalty.Application.ViewModels.LoyaltyProgram;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Application.ViewModels.Purchase;
using Loyalty.Application.ViewModels.Rule;
using Loyalty.Application.ViewModels.UserProfile;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Domain.Handlers.Firebase.Queries.QueryResults;
using Loyalty.Domain.Handlers.Queries.Commands.Locations;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using Loyalty.Domain.Handlers.Queries.Commands.UserProfile;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers.Invites;
using Loyalty.Domain.Handlers.Queries.QueryResults.Location;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.Purchase;
using Loyalty.Domain.Handlers.Queries.QueryResults.Rules;
using Loyalty.Domain.Handlers.Queries.QueryResults.UserProfile;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;

namespace Loyalty.Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapVenue();
            MapWorker();
            MapProductsAndGroups();
            MapLoyaltyPrograms();

            CreateMap<ProductPurchaseResult, ProductPurchaseViewModel>();
            CreateMap<GroupPurchaseResult, GroupPurchaseViewModel>();
            CreateMap<GetActivePurchaseResult, ActivePurchasesViewModel>();

            CreateMap<UserProfileViewModel, UpdateUserProfileCommand>()
                .ForMember(x => x.WorkerId, opt => opt.Ignore())
                .ForSourceMember(x => x.PositionName, opt => opt.DoNotValidate());

            CreateMap<GetUserProfileByIdQueryResult, FullUserProfileViewModel>();

            CreateMap<GetClientInfoFirebaseQueryResult, ClientInfoViewModel>();
        }

        private void MapLoyaltyPrograms()
        {
            CreateMap<GetRuleByIdQueryResult, RuleViewModel>();
            CreateMap<GetSingleRuleByIdQueryResult, SingleRuleViewModel>();
            CreateMap<GetLoyaltyProgramByIdQueryResult, LoyaltyProgramViewModel>();
            CreateMap<LoyaltyProgramViewModel, CreateLoyaltyProgramCommand>()
                .ForMember(x => x.UserId, opt => opt.Ignore())
                .ForMember(x => x.VenueId, opt => opt.Ignore())
                .ForSourceMember(x => x.IsPublished, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate());

            CreateMap<LoyaltyProgramViewModel, UpdateLoyaltyProgramCommand>()
                .ForMember(x => x.UserId, opt => opt.Ignore())
                .ForMember(x => x.VenueId, opt => opt.Ignore());

            CreateMap<GetLoyaltyProductGroupByIdQueryResult, LoyaltyProductGroupGetViewModel>();
        }

        private void MapProductsAndGroups()
        {
            CreateMap<GetProductByIdQueryResult, GetProductViewModel>()
                .ForSourceMember(x => x.IsArchived, opt => opt.DoNotValidate());

            CreateMap<CreateProductViewModel, CreateProductCommand>();

            CreateMap<UpdateProductViewModel, UpdateProductCommand>();

            CreateMap<GetProductGroupByIdQueryResult, ProductGroupViewModel>();

            CreateMap<GetProductViewModel, CreateProductCommand>()
                .ForMember(x => x.ProductGroupId, opt => opt.Ignore())
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate());

            CreateMap<ProductGroupViewModel, CreateProductGroupCommand>()
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate());

            CreateMap<ProductGroupViewModel, UpdateProductGroupCommand>();

            CreateMap<PatchProductViewModel, PatchProductCommand>();
            CreateMap<ProductGroupPatchViewModel, PatchProductGroupCommand>();

            CreateMap<GetProductViewModel, UpdateProductCommand>()
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate());
        }

        private void MapWorker()
        {
            CreateMap<GetWorkerByIdQueryResult, WorkerViewModel>();
            CreateMap<InviteViewModel, CreateInviteCommand>();
            CreateMap<UpdateInviteViewModel, UpdateInviteCommand>();
            CreateMap<GetVenueWorkerResult, VenueWorkerViewModel>();
        }

        private void MapVenue()
        {
            CreateMap<GetVenueWorkingHoursQueryResult, WorkingHoursViewModel>();
            CreateMap<WorkingHoursViewModel, WorkingHoursCommand>();

            CreateMap<LocationViewModel, CreateLocationCommand>();
            CreateMap<LocationViewModel, UpdateLocationCommand>();

            CreateMap<GetLocationQueryResult, LocationViewModel>();

            CreateMap<GetVenueByIdQueryResult, UpdateVenueViewModel>();

            CreateMap<CreateVenueViewModel, CreateVenueCommand>()
                .ForSourceMember(x => x.LogoUrl, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.Images, opt => opt.DoNotValidate());

            CreateMap<UpdateVenueViewModel, UpdateVenueCommand>()
                .ForSourceMember(x => x.LogoUrl, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.Images, opt => opt.DoNotValidate());

            CreateMap<GetSocialNetworksResult, SocialNetworksViewModel>();
            CreateMap<SocialNetworksViewModel, UpdateSocialNetworksCommand>();
            CreateMap<SocialNetworksViewModel, CreateSocialNetworksCommand>();
        }
    }
}