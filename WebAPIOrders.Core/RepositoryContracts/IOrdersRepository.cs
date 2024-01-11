using WebAPIOrders.Core.Entities;

namespace WebAPIOrders.Core.RepositoryContracts
{
	public interface IOrdersRepository
	{
		public Task<List<Order>> GetAllOrders();
		public Task<Order?> GetOrderByID(Guid guid);
		public Task<Order> CreateOrder(Order newOrder);
		public Task<Order> UpdateOrder(Order updatedOrder);
		public Task<bool> DeleteOrder(Guid guid);
	}
}
