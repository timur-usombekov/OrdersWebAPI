using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIOrders.Core.DTO.Orders;

namespace WebAPIOrders.Core.DTO.OrderItems
{
	public class OrderItemResponse
	{
		public Guid Id { get; set; }
		public Guid OrderId { get; set; }
		public string ProductName { get; set; } = null!;
		public long Quantity { get; set; }
		public double UnitPrice { get; set; }
		public double TotalPrice { get; set; }
	}
}
