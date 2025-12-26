using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class ArchiveWorkerCommandHandler
        : BaseDapperHandler, IArchiveWorkerCommandHandler
    {
        private readonly SqlConnection connection;

        public ArchiveWorkerCommandHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public Task<ICommandResult> Handle(ArchiveWorkerCommand request, CancellationToken cancellationToken)
        {
            var ids = Principal.GetVenueIds();
            var id = request.Id;

            ICommandResult result = null;
            var updateSql = "UPDATE loyalty.Worker SET [IsArchived] = 1 WHERE Id = @id";
            var deleteSql = "DELETE FROM loyalty.VenueWorker WHERE WorkerId = @id AND VenueId in @ids";

            connection.Open();

            using (var scope = new TransactionScope())
            {
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