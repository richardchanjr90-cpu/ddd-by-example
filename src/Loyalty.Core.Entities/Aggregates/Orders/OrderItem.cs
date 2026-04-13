using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Aggregates.Products;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities.Aggregates.Orders
{
    public class OrderItem : TenantEntity
    {
        public OrderItem(
            long id,
            int amount,
            long productId,
            long orderId)
        {
            Id = id;
            Amount = amount;
            ProductId = productId;
            OrderId = orderId;
        }

        private OrderItem()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public new long Id { get; set; }

        public override long TenantId => Order.TenantId;

        public int Amount { get; private set; }

        [ForeignKey(nameof(Product))]
        public long ProductId { get; private set; }

        public Product Product { get; private set; }

        public Order Order { get; private set; }

        [ForeignKey(nameof(Order))]
        public long OrderId { get; private set; }
    }
}
