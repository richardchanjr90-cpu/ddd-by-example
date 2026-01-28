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
        public string UserId { get; set; }

        [Required]
        public string CodeValue { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
