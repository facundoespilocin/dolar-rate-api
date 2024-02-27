using Ecommerce.DataAccessLayer.Entities.Order;
using Ecommerce.DataAccessLayer.Models;

namespace Ecommerce.DataAccessLayer.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        Task<OrderDTO> GetById(long orderId);
        Task<ServiceResponse> PostCreateOrder(CreateOrderDTO request, long userId);
        Task<ServiceResponse> PutAddItemsToOrder(CreateOrderDTO request, long userId);
        Task<ServiceResponse> DeleteOrderItem(RemoveOrderItemDTO request, long userId);
        Task<ServiceResponse> DeleteOrder(long orderId, long organizationId);
        Task<ServiceResponse> PutFinishOrder(UpdateOrderDTO request, long orderId, long userId);
    }
}