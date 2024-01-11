using System.ComponentModel.DataAnnotations;

namespace WebAPIOrders.Core.Entities
{
	public class Order
	{
		[Required]
		[Key]
		public Guid Id { get; set; }
		public string Number { get; set; } = null!;
		[Required]
		[MaxLength(50)]
		public string CustomerName { get; set; } = null!;
		[Required]
		public DateTime OrderDate { get; set; }
		[Range(0, double.MaxValue)]
		public double TotalAmount { get; set; }

		public ICollection<OrderItem> OrderItems { get; set; } = null!;
	}
}
