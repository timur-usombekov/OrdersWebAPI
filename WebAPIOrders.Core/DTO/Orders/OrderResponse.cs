using System;
using System.Collections.Generic;
using WebAPIOrders.Core.DTO.OrderItems;


namespace WebAPIOrders.Core.DTO.Orders
{
	public class OrderResponse
	{
		public Guid Id { get; set; }
		public string Number { get; set; } = null!;
		public string CustomerName { get; set; } = null!;
		public DateTime OrderDate { get; set; }
		public double TotalAmount { get; set; }

		public List<OrderItemResponse> OrderItems { get; set; } = null!;
	}
}
