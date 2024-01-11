using WebAPIOrders.Core.DTO.OrderItems;
using WebAPIOrders.Core.Entities;
using WebAPIOrders.Core.RepositoryContracts;
using WebAPIOrders.Core.ServiceContracts;

namespace WebAPIOrders.Core.Services
{
	public class OrderItemsService : IOrderItemsService
	{
		private readonly IOrderItemsRepository _orderItemsRepository;
		private readonly IOrdersService _ordersService;

		public OrderItemsService(IOrderItemsRepository orderItemsRepository, IOrdersService ordersService)
		{
			_orderItemsRepository = orderItemsRepository;
			_ordersService = ordersService;
		}
		public async Task<OrderItemResponse> CreateOrderItem(AddOrderItemRequest addOrderItemRequest)
		{
			OrderItem ordItm = new()
			{
				Id = Guid.NewGuid(),
				OrderId = addOrderItemRequest.OrderId,
				ProductName = addOrderItemRequest.ProductName,
				Quantity = addOrderItemRequest.Quantity,
				UnitPrice = addOrderItemRequest.UnitPrice,
				TotalPrice = addOrderItemRequest.Quantity * addOrderItemRequest.UnitPrice
			};
			await _orderItemsRepository.CreateOrderItem(ordItm);
			return ordItm.ToOrderItemResponse();
		}

		public async Task<bool> DeleteOrderItem(Guid orderItemId)
		{
			var ordItm = await _orderItemsRepository.GetOrderItemByID(orderItemId);
			if(ordItm != null)
			{
				return await _orderItemsRepository.DeleteOrderItem(ordItm.Id);
			}
			return false;
		}

		public async Task<List<OrderItemResponse>> GetOrderedItemsForOrder(Guid orderId)
		{
			var orderItems = await _orderItemsRepository.GetOrderItems();
			var orderItemsForOrder = orderItems.Where(ordItm => ordItm.OrderId == orderId).ToList();

			return orderItemsForOrder.Select(itm => itm.ToOrderItemResponse()).ToList();
		}

		public async Task<OrderItemResponse?> GetOrderItemByID(Guid guid)
		{
			var ordItm = await _orderItemsRepository.GetOrderItemByID(guid);
			return ordItm?.ToOrderItemResponse();
		}

		public async Task<List<OrderItemResponse>> GetOrderItems()
		{
			var items = await _orderItemsRepository.GetOrderItems();
			return items.Select(itm => itm.ToOrderItemResponse()).ToList();
		}

		public async Task<OrderItemResponse> UpdateOrderItem(UpdateOrderItemRequest updateOrderItemRequest)
		{
			var oldItm = await _orderItemsRepository.GetOrderItemByID(updateOrderItemRequest.OrderItemId);
			if (oldItm == null)
			{
				throw new NullReferenceException("Order was not found");
			}
			oldItm.Quantity = updateOrderItemRequest.Quantity ?? oldItm.Quantity;
			oldItm.UnitPrice = updateOrderItemRequest.UnitPrice ?? oldItm.UnitPrice;
			oldItm.TotalPrice = oldItm.Quantity * oldItm.UnitPrice;
			await _orderItemsRepository.UpdateOrderItem(oldItm);
			return oldItm.ToOrderItemResponse();
		}
	}
	public static partial class Exetension
	{
		public static OrderItemResponse ToOrderItemResponse(this OrderItem orderItem)
		{
			var resp = new OrderItemResponse()
			{
				Id = orderItem.Id,
				OrderId = orderItem.OrderId,
				ProductName = orderItem.ProductName,
				Quantity = orderItem.Quantity,
				UnitPrice = orderItem.UnitPrice,
				TotalPrice = orderItem.TotalPrice,
			};
			return resp;
		}
	}
}
