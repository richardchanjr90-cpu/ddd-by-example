using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Loyalty.Application.ViewModels.Signup;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using MediatR;

namespace Loyalty.Application.Venue
{
    public class LoyaltySignupAppService : BaseAppService
    {
        private readonly IMapper mapper;

        public LoyaltySignupAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<WorkerViewModel> Get(SignupViewModel model, string phone)
        {
            var result = await Mediator.Send(new GetWorkerByPhoneQuery()
            {
                Phone = phone
            });

            return mapper.Map<WorkerViewModel>(result);
        }
    }
}
