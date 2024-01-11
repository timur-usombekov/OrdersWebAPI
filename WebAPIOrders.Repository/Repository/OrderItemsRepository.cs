using Microsoft.EntityFrameworkCore;
using WebAPIOrders.Core.Entities;
using WebAPIOrders.Core.RepositoryContracts;

namespace WebAPIOrders.Repository.Repository
{
	public class OrderItemsRepository: IOrderItemsRepository
	{
		private readonly ApplicationContext _db;
		public OrderItemsRepository(ApplicationContext db)
		{
			_db = db;
		}

		public async Task<OrderItem> CreateOrderItem(OrderItem orderItem)
		{
			_db.OrderItems.Add(orderItem);
			await _db.SaveChangesAsync();
			return orderItem;
		}

		public async Task<bool> DeleteOrderItem(Guid guid)
		{
			var ord = _db.OrderItems.FirstOrDefault(or => or.Id == guid);
			if (ord == null)
			{
				return false;
			}
			_db.OrderItems.Remove(ord);
			await _db.SaveChangesAsync();

			return true;
		}

		public async Task<OrderItem?> GetOrderItemByID(Guid guid)
		{
			return await _db.OrderItems.FirstOrDefaultAsync(or => or.Id == guid);
		}

		public async Task<List<OrderItem>> GetOrderItems()
		{
			return await _db.OrderItems.ToListAsync();
		}

		public async Task<OrderItem> UpdateOrderItem(OrderItem updateOrderItem)
		{
			_db.OrderItems.Update(updateOrderItem);
			await _db.SaveChangesAsync();
			return updateOrderItem;
		}
	}
}
