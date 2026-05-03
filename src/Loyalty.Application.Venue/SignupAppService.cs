using System;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using FluentValidation;
using Loyalty.Application.ViewModels.Signup;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Application.Venue
{
    public class SignupAppService : BaseAppService
    {
        public SignupAppService(
            IMediator mediator)
            : base(mediator)
        {
        }
        
        public async Task<ICommandResult> StartSignup(SignupViewModel model, FunctionTokenResult token)
        {
            new SignupViewModelValidator()
                .ValidateAndThrow(model);

            var result = await Mediator.Send(new CreateWorkerCommand()
            {
                Name = model.Name,
                Surname = model.Surname,
                City = model.City
            });

            return result;
        }
    }
}
