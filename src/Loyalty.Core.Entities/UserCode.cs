using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities
{
    [Table("UserCode", Schema = SchemaName.Loyalty)]
    public class UserCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }

        public string CodeValue { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
