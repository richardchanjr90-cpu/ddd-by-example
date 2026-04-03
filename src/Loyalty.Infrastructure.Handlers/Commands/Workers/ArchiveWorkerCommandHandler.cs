using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Contracts.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class ArchiveWorkerCommandHandler
        : BaseDapperHandler, IArchiveWorkerCommandHandler
    {
        private readonly SqlConnection connection;
        private readonly IMediator mediator;

        public ArchiveWorkerCommandHandler(SqlConnection connection, IHttpContextAccessor accessor, IMediator mediator)
            : base(connection, accessor)
        {
            this.connection = connection;
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(ArchiveWorkerCommand request, CancellationToken cancellationToken)
        {
            var role = Principal.GetRole();
            var venueId = request.VenueId;

            var id = request.Id;

            //var updateSql = "UPDATE loyalty.Worker SET [IsArchived] = 1 WHERE Id = @id";
            var deleteSql = "DELETE FROM loyalty.VenueWorker WHERE WorkerId = @id AND VenueId = @venueId AND [Role] < @role";

            ICommandResult result = null;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await connection.OpenAsync(cancellationToken);

                var number = await connection.ExecuteAsync(deleteSql, new
                {
                    id,
                    venueId,
                    role
                });

                if (number > 0)
                {
                    //var number2 = await connection.ExecuteAsync(updateSql, new
                    //{
                    //    id
                    //});
                    scope.Complete();

                    result = new CommandResult
                    {
                        Success = true,
                        Result = id
                    };
                }
                else
                {
                    result = new CommandResult
                    {
                        Success = false,
                    };
                }

                return result;
            }
        }
    }
}