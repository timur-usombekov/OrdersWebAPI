using WebAPIOrders.Core.DTO.OrderItems;
using WebAPIOrders.Core.Entities;

namespace WebAPIOrders.Core.RepositoryContracts
{
	public interface IOrderItemsRepository
	{
		public Task<List<OrderItem>> GetOrderItems();
		public Task<OrderItem?> GetOrderItemByID(Guid guid);
		public Task<OrderItem> CreateOrderItem(OrderItem orderItem);
		public Task<OrderItem> UpdateOrderItem(OrderItem updateOrderItem);
		public Task<bool> DeleteOrderItem(Guid guid);
	}
}
