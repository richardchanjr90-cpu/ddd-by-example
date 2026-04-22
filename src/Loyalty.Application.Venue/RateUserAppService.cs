using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.ViewModels.Rate;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Orders;
using Loyalty.Shared.Contracts.Enums;
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

        public async Task<ICommandResult> Rate(RateViewModel model, long id)
        {
            new RateValidator()
                .ValidateAndThrow(model);

            return await Mediator.Send(new PatchRateOrderCommand()
            {
                Comment =  model.Comment,
                Rate =  (OrderVenueRate)model.Rate,
                OrderId = id,
            });

            return new CommandResult
            {
                Success = true
            };
        }
    }
}
