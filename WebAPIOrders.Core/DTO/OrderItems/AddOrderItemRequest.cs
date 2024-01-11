using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIOrders.Core.Entities;

namespace WebAPIOrders.Core.DTO.OrderItems
{
	public class AddOrderItemRequest
	{
		public Guid OrderId { get; set; }
		[Required]
		[StringLength(50)]
		public string ProductName { get; set; } = null!;
		[Range(0, long.MaxValue)]
		public long Quantity { get; set; }
		[Range(0, long.MaxValue)]
		public double UnitPrice { get; set; }
	}
}
