using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIOrders.Core.DTO.OrderItems;

namespace WebAPIOrders.Core.ServiceContracts
{
	public interface IOrderItemsService
	{
		public Task<List<OrderItemResponse>> GetOrderItems();
		public Task<OrderItemResponse?> GetOrderItemByID(Guid guid);
		public Task<List<OrderItemResponse>> GetOrderedItemsForOrder(Guid orderId);
		public Task<OrderItemResponse> CreateOrderItem(AddOrderItemRequest addOrderItemRequest);
		public Task<OrderItemResponse> UpdateOrderItem(UpdateOrderItemRequest updateOrderItemRequest);
		public Task<bool> DeleteOrderItem(Guid orderItemId);
	}
}
