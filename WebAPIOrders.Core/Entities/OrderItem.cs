using System.ComponentModel.DataAnnotations;

namespace WebAPIOrders.Core.Entities
{
	public class OrderItem
	{
		[Key]
		public Guid Id { get; set; }
		[Required]
		public Guid OrderId { get; set; }
		[Required]
		[StringLength(50)]
		public string ProductName { get; set; } = null!;
		[Range(0, long.MaxValue)]
		public long Quantity { get; set; }
		[Range(0, long.MaxValue)]
		public double UnitPrice { get; set; }
		public double TotalPrice { get; set; }


		public Order Order { get; set; } = null!;
		public OrderItem() 
		{
			TotalPrice = Quantity * UnitPrice;
		}

	}
}
