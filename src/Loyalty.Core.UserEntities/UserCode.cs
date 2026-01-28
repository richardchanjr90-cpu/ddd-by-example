using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.UserEntities.Schema;

namespace Loyalty.Core.UserEntities
{
    [Table("UserCode", Schema = SchemaName.User)]
    public class UserCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }

        public string CodeValue { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
