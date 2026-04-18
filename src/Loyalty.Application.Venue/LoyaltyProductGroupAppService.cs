using System;
using System.Collections.Generic;
using System.Text.Json;
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
using System.Text.Json.Serialization;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.SeedWork;
using MediatR.Extensions.UnitOfWork.Interface;

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

        public async Task<LoyaltyProductGroupGetViewModel> Get(long id, long programId)
        {
            var result = await Mediator.Send(new GetLoyaltyProductGroupByIdQuery
            {
                Id = id,
                ProgramId = programId
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

        public async Task<ICommandResult> Archive(long id, string userId)
        {
            var command = new ArchiveLoyaltyProductGroupCommand
            {
                Id = id,
                UserId = userId
            };

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        private void TransformRules(LoyaltyProductGroupViewModel model, Action<object, SingleRuleViewModel> action)
        {
            var nameSpace = "Loyalty.Core.Entities.Rules";
                //todo: this should be tested
            foreach (var rule in model.Rules.Rules)
            {
                var name = Enum.GetName(typeof(LoyaltyRuleType), rule.RuleType);
                var type = typeof(Entity).Assembly.GetType($"{nameSpace}.{name}Rule{rule.RuleVersion}");
                var result = JsonSerializer.Deserialize(rule.Rule, type);

                action?.Invoke(result, rule);
            }
        }
    }
}