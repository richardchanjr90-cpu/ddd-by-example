using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.ViewModels.Rate;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Notifications.Rate;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Application.Venue
{
    public class RateUserAppService: BaseAppService
    {
        public RateUserAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
        }

        public async Task<ICommandResult> Rate(RateViewModel model)
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
