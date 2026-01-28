using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.UserEntities.Schema;

namespace Loyalty.Core.UserEntities
{
    [Table("User", Schema = SchemaName.User)]
    public class User
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string PhotoUrl { get; set; }
    }
}
