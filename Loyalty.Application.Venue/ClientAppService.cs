using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.ViewModels;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Queries.Client;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Client;
using MediatR;

namespace Loyalty.Application.Venue
{
    public class ClientAppService : BaseAppService
    {
        private readonly IMapper mapper;

        public ClientAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<ClientViewModel> Create(string userCode)
        {
            //todo: validation
            //todo: convert code to guid

            var result = await Mediator.Send(new GetClientByCodeQuery
            {
                UserCode = userCode
            });

            return mapper.Map<ClientViewModel>(result);
        }

    }
}
