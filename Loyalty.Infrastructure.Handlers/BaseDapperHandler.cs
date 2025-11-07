using System.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers
{
    public abstract class BaseDapperHandler
    {
        protected BaseDapperHandler(SqlConnection connection)
        {
            Connection = connection;
        }

        public SqlConnection Connection { get; }
    }
}