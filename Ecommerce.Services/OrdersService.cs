using AutoMapper;
using Ecommerce.DataAccessLayer.Entities.Order;
using Ecommerce.DataAccessLayer.Models;
using Ecommerce.DataAccessLayer.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Utils.Settings;
using Microsoft.Extensions.Options;

namespace Ecommerce.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public OrdersService(IOrdersRepository ordersRepository, IOptions<AppSettings> appSettings, IMapper mapper, IProductsRepository productsRepository)
        {
            _ordersRepository = ordersRepository;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _productsRepository = productsRepository;
        }

        public async Task<OrderDTO> GetById(long orderId)
        {
            var order = await _ordersRepository.GetById(orderId);

            return order;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllByCustomerId(long customerId)
        {
            var order = await _ordersRepository.GetAllByCustomerId(customerId);

            return order;
        }

        public async Task<ServiceResponse> PostCreateOrder(CreateOrderDTO request, long userId)
        {
            var response = await _ordersRepository.PostCreateOrder(request, userId);

            return response;
        }

        public async Task<ServiceResponse> PutAddItemsToOrder(CreateOrderDTO request, long userId)
        {
            var response = await _ordersRepository.PutAddItemsToOrder(request, userId);

            return response;
        }

        public async Task<ServiceResponse> DeleteOrderItem(RemoveOrderItemDTO request, long userId)
        {
            var response = await _ordersRepository.DeleteOrderItem(request, userId);

            return response;
        }

        public async Task<ServiceResponse> DeleteOrder(long orderId, long organizationId)
        {
            var response = await _ordersRepository.DeleteOrder(orderId, organizationId);

            return response;
        }

        public async Task<ServiceResponse> PutFinishOrder(UpdateOrderDTO request, long orderId, long userId)
        {
            var response = await _ordersRepository.PutFinishOrder(request, orderId, userId);

            return response;
        }
    }
}