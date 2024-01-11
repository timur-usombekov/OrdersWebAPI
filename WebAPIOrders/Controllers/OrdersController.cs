using Microsoft.AspNetCore.Mvc;
using System;
using WebAPIOrders.Core.DTO.OrderItems;
using WebAPIOrders.Core.DTO.Orders;
using WebAPIOrders.Core.ServiceContracts;
using WebAPIOrders.Repository;

namespace WebAPIOrders.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrdersService _orderService;
		private readonly IOrderItemsService _orderItemsService;
		public OrdersController(IOrdersService context, IOrderItemsService orderItemsService)
		{
			_orderService = context;
			_orderItemsService = orderItemsService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrders()
		{
			return await _orderService.GetOrders();
		}

		[HttpGet("{guid}")]
		public async Task<ActionResult<OrderResponse>> GetOrderByID(Guid guid)
		{
			var order = await _orderService.GetOrderByID(guid);
			if (order == null)
			{
				return NotFound();
			}
			return order;
		}
		[HttpPost]
		public async Task<ActionResult<OrderResponse>> PostOrder(AddOrderRequest addOrderRequest)
		{
			var order = await _orderService.CreateNewOrder(addOrderRequest);
			return CreatedAtAction(nameof(GetOrderByID), new { guid = order.Id}, order);
		}
		[HttpPut("{guid}")]
		public async Task<IActionResult> PutOrder(Guid guid,[FromBody] UpdateOrderRequest updateOrderRequest)
		{
			if(guid != updateOrderRequest.OrderId)
			{
				return BadRequest();
			}
			if(await _orderService.GetOrderByID(updateOrderRequest.OrderId) != null) 
			{
				await _orderService.UpdateOrder(updateOrderRequest);
				return Ok();
			}
			return NotFound();
		}
		[HttpDelete("{guid}")]
		public async Task<IActionResult> DeleteOrder(Guid guid)
		{
			if(await _orderService.DeleteOrder(guid))
			{
				return NoContent();
			}
			return NotFound();
		}
		[HttpGet("{guid}/items")]
		public async Task<ActionResult<List<OrderItemResponse>>> GetOrderItem(Guid guid)
		{
			var order = await _orderService.GetOrderByID(guid);
			if (order == null)
			{
				return NotFound();
			}
			return await _orderItemsService.GetOrderedItemsForOrder(guid);
		}
		[HttpGet("{orderId}/items/{itemId}")]
		public async Task<ActionResult<OrderItemResponse>> GetOrderItemById(Guid orderId, Guid itemId)
		{
			var order = await _orderService.GetOrderByID(orderId);
			if (order == null)
			{
				return NotFound();
			}
			var item = await _orderItemsService.GetOrderItemByID(itemId);
			if (item == null)
			{
				return NotFound();
			}
			return item;
		}
		[HttpPost("{orderId}/items")]
		public async Task<ActionResult<OrderItemResponse>> PostOrderItem(Guid orderId, [FromBody] AddOrderItemRequest addOrderItemRequest)
		{
			if(orderId != addOrderItemRequest.OrderId)
			{
				return BadRequest();
			}
			var item = await _orderItemsService.CreateOrderItem(addOrderItemRequest);
			return CreatedAtAction(nameof(GetOrderItemById), new { orderId = item.OrderId, itemId = item.Id},item);
		}
		[HttpPut("{orderId}/items/{itemId}")]
		public async Task<ActionResult<OrderItemResponse>> PutOrderItem(Guid orderId,Guid itemId, 
			[FromBody] UpdateOrderItemRequest updateOrderItemRequest)
		{
			var orderItem = await _orderItemsService.GetOrderItemByID(itemId);
			if(orderItem == null)
			{
				return NotFound();
			}
			if (orderItem.OrderId != orderId || orderItem.Id != itemId)
			{
				return BadRequest();
			}
			return Ok(await _orderItemsService.UpdateOrderItem(updateOrderItemRequest));
		}
		[HttpDelete("{orderId}/items/{itemId}")]
		public async Task<ActionResult<OrderItemResponse>> DeleteOrderItem(Guid orderId, Guid itemId)
		{
			var orderItem = await _orderItemsService.GetOrderItemByID(itemId);
			if (orderItem == null)
			{
				return NotFound();
			}
			if (orderItem.OrderId != orderId || orderItem.Id != itemId)
			{
				return BadRequest();
			}
			await _orderItemsService.DeleteOrderItem(itemId);
			return NoContent();
		}
	}
}
