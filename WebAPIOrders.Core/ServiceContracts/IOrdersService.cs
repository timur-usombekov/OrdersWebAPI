using WebAPIOrders.Core.DTO.Orders;

namespace WebAPIOrders.Core.ServiceContracts
{
	public interface IOrdersService
	{
		public Task<List<OrderResponse>> GetOrders();
		public Task<OrderResponse?> GetOrderByID(Guid guid);
		public Task<OrderResponse> CreateNewOrder(AddOrderRequest addOrderRequest);
		public Task<OrderResponse> UpdateOrder(UpdateOrderRequest updateOrderRequest);
		public Task<bool> DeleteOrder(Guid guid);
		
	}
}
