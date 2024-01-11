using WebAPIOrders.Core.DTO.Orders;
using WebAPIOrders.Core.DTO.OrderItems;
using WebAPIOrders.Core.Entities;
using WebAPIOrders.Core.RepositoryContracts;
using WebAPIOrders.Core.ServiceContracts;

namespace WebAPIOrders.Core.Services
{
	public class OrdersService : IOrdersService
	{
		private readonly IOrdersRepository _ordersRepository;

		public OrdersService(IOrdersRepository ordersRepository)
		{
			_ordersRepository = ordersRepository;
		}

		public async Task<bool> DeleteOrder(Guid guid)
		{
			var order = await _ordersRepository.GetOrderByID(guid);
			if (order != null)
			{
				return await _ordersRepository.DeleteOrder(guid);
			}
			return false;
		}
		public async Task<OrderResponse?> GetOrderByID(Guid guid)
		{
			var order = await _ordersRepository.GetOrderByID(guid);
			if (order != null)
			{
				return order.ToOrderResponse();
			}
			return null;
		}
		public async Task<List<OrderResponse>> GetOrders()
		{
			var orders = await _ordersRepository.GetAllOrders();
			return orders.Select(ord => ord.ToOrderResponse()).ToList();
		}
		public async Task<OrderResponse> CreateNewOrder(AddOrderRequest addOrderRequest)
		{
			Order order = new Order()
			{
				Id = Guid.NewGuid(),
				OrderDate = DateTime.Now,
				CustomerName = addOrderRequest.CustomerName,
				Number = DateTime.Now.Year.ToString() + _ordersRepository.GetAllOrders().Result.Count().ToString(),  
				TotalAmount = 0,
			};
			await _ordersRepository.CreateOrder(order);
			return order.ToOrderResponse();
		}
		public async Task<OrderResponse> UpdateOrder(UpdateOrderRequest updateOrderRequest)
		{
			var oldOrd = await _ordersRepository.GetOrderByID(updateOrderRequest.OrderId);
			if(oldOrd == null)
			{
				throw new NullReferenceException("Order was not found");
			}
			oldOrd.CustomerName = updateOrderRequest.CustomerName;
			await _ordersRepository.UpdateOrder(oldOrd);
			return oldOrd.ToOrderResponse();
		}
	}

	public static partial class Exetension
	{
		public static OrderResponse ToOrderResponse(this Order order) 
		{
			var resp = new OrderResponse()
			{
				Id = order.Id,
				OrderDate = order.OrderDate,
				CustomerName = order.CustomerName,
				Number = order.Number,
				TotalAmount = order.TotalAmount,
				OrderItems = order.OrderItems?.Where(ordItems => ordItems.OrderId == order.Id)
					.Select(itm => itm.ToOrderItemResponse())
					.ToList() 
				?? new List<OrderItemResponse>() { },
				
			};
			return resp;
		}
	}
}
