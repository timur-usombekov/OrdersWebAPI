using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIOrders.Core.Entities;

namespace WebAPIOrders.Core.DTO.Orders
{
	public class UpdateOrderRequest
	{
		[Required]
		public Guid OrderId { get; set; }
		[Required]
		[MaxLength(50)]
		public string CustomerName { get; set; } = null!;
	}
}
