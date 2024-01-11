using Microsoft.EntityFrameworkCore;
using WebAPIOrders.Core.Entities;
using WebAPIOrders.Core.RepositoryContracts;

namespace WebAPIOrders.Repository.Repository
{
	public class OrdersRepository : IOrdersRepository
	{
		private readonly ApplicationContext _db;
		public OrdersRepository(ApplicationContext db)
		{
			_db = db;
		}

		public async Task<Order> CreateOrder(Order newOrder)
		{
			_db.Orders.Add(newOrder);
			await _db.SaveChangesAsync();
			return newOrder;
		}

		public async Task<bool> DeleteOrder(Guid guid)
		{
			var ord = _db.Orders.FirstOrDefault(or => or.Id == guid);
			if (ord == null) 
			{ 
			 return false;
			}
			_db.Orders.Remove(ord);
			await _db.SaveChangesAsync();

			return true;
		}

		public async Task<List<Order>> GetAllOrders()
		{
			return await _db.Orders.Include(ord => ord.OrderItems).ToListAsync();
		}

		public async Task<Order?> GetOrderByID(Guid guid)
		{
			return await _db.Orders.Include(ord => ord.OrderItems).FirstOrDefaultAsync(or => or.Id == guid);
		}

		public async Task<Order> UpdateOrder(Order updatedOrder)
		{
			_db.Orders.Update(updatedOrder);
			await _db.SaveChangesAsync();
			return updatedOrder;
		}
	}
}
