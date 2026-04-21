using System;
using System.Threading;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Firebase.Queries.Commands.User;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Firebase.Handlers.Commands.User
{
    public class UpdateUserEmailCommandHandler
        : BaseFirebaseHandler, IRequestHandler<UpdateUserEmailCommand, ICommandResult>
    {
        private readonly IHttpContextAccessor accessor;

        public UpdateUserEmailCommandHandler(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public async Task<ICommandResult> Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
        {
            var result = false;
            if (!String.IsNullOrEmpty(request.NewEmail) && !request.NewEmail.Equals(request.CurrentEmail))
            {
                var args = new UserRecordArgs
                {
                    Email = request.NewEmail,
                    Uid = request.UserId,
                    EmailVerified = false
                };

                await FirebaseAuth.DefaultInstance.UpdateUserAsync(args, cancellationToken);
                result = true;
            }
            else if (!String.IsNullOrEmpty(request.NewEmail) && !request.IsEmailVerified)
            {
                result = true;
            }

            return new CommandResult
            {
                Success = result
            };
        }
    }
}