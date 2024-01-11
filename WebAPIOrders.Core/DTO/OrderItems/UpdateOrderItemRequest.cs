using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIOrders.Core.DTO.OrderItems
{
	public class UpdateOrderItemRequest
	{
		[Required]
		public Guid OrderItemId { get; set; }
		[StringLength(50)]
		public string? ProductName { get; set; }
		[Range(0, long.MaxValue)]
		public long? Quantity { get; set; }
		[Range(0, long.MaxValue)]
		public double? UnitPrice { get; set; }
	}
}
