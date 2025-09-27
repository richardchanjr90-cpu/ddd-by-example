using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;
using Loyalty.Common.Shared.Enums;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.Commands.Rules;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProductGroup;
using MediatR;
using Newtonsoft.Json;

namespace Loyalty.Application.Venue
{
    public class LoyaltyProductGroupAppService : BaseAppService
    {
        private readonly IMapper mapper;

        public LoyaltyProductGroupAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<LoyaltyProductGroupViewModel> Get(long id)
        {
            var result = await Mediator.Send(new GetLoyaltyProductGroupByIdQuery
            {
                Id = id
            });

            return mapper.Map<LoyaltyProductGroupViewModel>(result);
        }

        public async Task<List<LoyaltyProductGroupViewModel>> GetAll(long loyaltyProgramId)
        {
            var result = await Mediator.Send(new GetLoyaltyProductGroupQuery
            {
                LoyaltyProgramId = loyaltyProgramId
            });
            return mapper.Map<List<LoyaltyProductGroupViewModel>>(result.Result);
        }

        public async Task<ICommandResult> Create(LoyaltyProductGroupViewModel model)
        {
            //new VenueValidator().ValidateAndThrow(model);
            var command = new CreateLoyaltyProductGroupCommand
            {
                Description = model.Description,
                LoyaltyProgramId = model.LoyaltyProgramId,
                Name = model.Name
            };

            //command.ProductGroup = model.ProductGroup;
            foreach (var rule in model.Rules.Rules)
            {
                string nameSpace = "Loyalty.Core.Entities.Rules";
                string assemblyName = "Loyalty.Core.Entities";
                string version = rule.RuleVersion.ToUpper();

                var name = Enum.GetName(typeof(LoyaltyRuleType), rule.RuleType);

                Type type = Type.GetType($"{nameSpace}.{name}Rule{version}, {assemblyName}");
                var result  = JsonConvert.DeserializeObject(rule.Rule, type);
                command.Rules.Add(new CreateSingleRuleCommand
                {
                    Rule = result,
                    RuleType = rule.RuleType,
                    RuleVersion = rule.RuleVersion
                });
            }

            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> Update(LoyaltyProductGroupViewModel model)
        {
            //new VenueValidator().ValidateAndThrow(model);

            var command = mapper.Map<UpdateLoyaltyProductGroupCommand>(model);
            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> Archive(long id)
        {
            var command = new ArchiveLoyaltyProductGroupCommand
            {
                Id = id,
                UserId = Guid.Parse("0abe336d-021c-40b5-ba95-909daeb7ca40")
            };

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }
    }
}
