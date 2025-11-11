using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;
using Loyalty.Application.ViewModels.Rule;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.Commands.Rules;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProductGroup;
using Loyalty.Shared.Contracts.Enums;
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

        public async Task<LoyaltyProductGroupGetViewModel> Get(long id)
        {
            var result = await Mediator.Send(new GetLoyaltyProductGroupByIdQuery
            {
                Id = id
            });

            return mapper.Map<LoyaltyProductGroupGetViewModel>(result);
        }

        public async Task<List<LoyaltyProductGroupGetViewModel>> GetAll(long loyaltyProgramId)
        {
            var result = await Mediator.Send(new GetLoyaltyProductGroupQuery
            {
                LoyaltyProgramId = loyaltyProgramId
            });
            return mapper.Map<List<LoyaltyProductGroupGetViewModel>>(result.Result);
        }

        public async Task<ICommandResult> Create(LoyaltyProductGroupViewModel model)
        {
            new LoyaltyProductGroupValidator().ValidateAndThrow(model);

            var command = new CreateLoyaltyProductGroupCommand
            {
                Description = model.Description,
                LoyaltyProgramId = model.LoyaltyProgramId,
                Name = model.Name,
                ProductGroupId = model.ProductGroupId
            };

            var ruleCommand = new CreateRuleCommand();

            TransformRules(model, (result, rule) =>
            {
                var singleCommand = new CreateSingleRuleCommand
                {
                    Rule = result,
                    RuleType = rule.RuleType,
                    RuleVersion = rule.RuleVersion
                };

                ruleCommand.Rules.Add(singleCommand);
            });

            command.Rule = ruleCommand;

            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> Update(LoyaltyProductGroupViewModel model)
        {
            new LoyaltyProductGroupValidator().ValidateAndThrow(model);

            var command = new UpdateLoyaltyProductGroupCommand
            {
                Description = model.Description,
                LoyaltyProgramId = model.LoyaltyProgramId,
                Name = model.Name,
                Id = model.Id,
                ProductGroupId = model.ProductGroupId
            };

            var ruleCommand = new UpdateRuleCommand();

            TransformRules(model, (result, rule) =>
            {
                var singleCommand = new UpdateSingleRuleCommand
                {
                    Rule = result,
                    RuleType = rule.RuleType,
                    RuleVersion = rule.RuleVersion
                };

                ruleCommand.Rules.Add(singleCommand);
            });

            command.Rule = ruleCommand;

            return await Mediator.Send(command);
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

        private void TransformRules(LoyaltyProductGroupViewModel model, Action<object, SingleRuleViewModel> action)
        {
            var nameSpace = "Loyalty.Core.Entities.Rules";
            var assemblyName = "Loyalty.Core.Entities";

            foreach (var rule in model.Rules.Rules)
            {
                var name = Enum.GetName(typeof(LoyaltyRuleType), rule.RuleType);

                var type = Type.GetType($"{nameSpace}.{name}RuleV{rule.RuleVersion}, {assemblyName}");
                var result = JsonConvert.DeserializeObject(rule.Rule, type);

                action?.Invoke(result, rule);
            }
        }
    }
}