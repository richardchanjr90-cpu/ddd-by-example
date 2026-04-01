using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities.Orders
{
    [Table("OrderItem", Schema = SchemaName.Loyalty)]
    public class OrderItem : TenantEntity
    {
        public override long TenantId => Order.TenantId;

        public int Amount { get; set; }

        [ForeignKey(nameof(Product))]
        public long ProductId { get; set; }

        public Product Product { get; set; }

        public Order Order { get; set; }

        [ForeignKey(nameof(Order))]
        public long OrderId { get; set; }
    }
}
