using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class ArchiveWorkerByUidCommandHandler
        : BaseDapperHandler, IRequestHandler<ArchiveWorkerByUidCommand, ICommandResult>
    {
        private readonly SqlConnection connection;

        public ArchiveWorkerByUidCommandHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public Task<ICommandResult> Handle(ArchiveWorkerByUidCommand request, CancellationToken cancellationToken)
        {
            var ids = Principal.GetVenueIds();
            var userId = request.UserId;

            ICommandResult result = null;
            var selectSql = "SELECT Id FROM loyalty.Worker WHERE WorkerId = @userId";
            var updateSql = "UPDATE loyalty.Worker SET [IsArchived] = 1 WHERE Id = @id";
            var deleteSql = "DELETE FROM loyalty.VenueWorker WHERE WorkerId = @id AND VenueId in @ids";

            using (var scope = new TransactionScope())
            {
                connection.Open();
                var id = connection.ExecuteScalar(selectSql, new
                {
                    userId
                });
                var number = connection.Execute(deleteSql, new
                {
                    id,
                    ids
                });

                if (number > 0)
                {
                    var number2 = connection.Execute(updateSql, new
                    {
                        id
                    });

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

                return Task.FromResult(result);
            }
        }
    }
}