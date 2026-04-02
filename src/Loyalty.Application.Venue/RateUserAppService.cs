using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.ViewModels.Purchase;
using Loyalty.Application.ViewModels.Rate;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Notifications.Rate;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using MediatR;
using MediatR.Extensions.UnitOfWork;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Application.Venue
{
    public class RateUserAppService: BaseAppService
    {
        public RateUserAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
        }

        public async Task<ICommandResult> Purchase(RateViewModel model)
        {
            new RateValidator()
                .ValidateAndThrow(model);

            await Mediator.Publish(
                new UpsertUserRateNotification
                {
                    VenueId = model.VenueId,
                    OrderId = model.OrderId,
                    Rate =  model.Rate,
                    UserId = model.UserId
                });

            return new CommandResult
            {
                Success = true
            };
        }
    }
}
