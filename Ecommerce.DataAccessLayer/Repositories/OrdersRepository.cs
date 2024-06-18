using Dapper;
using Ecommerce.DataAccessLayer.Entities.Order;
using Ecommerce.DataAccessLayer.Entities.Product;
using Ecommerce.DataAccessLayer.Factory;
using Ecommerce.DataAccessLayer.Models;
using Ecommerce.DataAccessLayer.Repositories.Interfaces;
using static Ecommerce.Utils.Enums;

namespace Ecommerce.DataAccessLayer.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private IConnectionFactory _factory;

        public OrdersRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<OrderDTO> GetById(long orderId)
        {
            using var con = _factory.GetDbConnection;

            var query = @$"SELECT 
                               o.Id, 
                               o.OrganizationId, 
                               o.CustomerId, 
                               o.PaymentMethodId,
                               o.Amount, 
                               o.Items, 
                               o.Discount,
                               o.Installments,
                               o.DeliveryType,
                               o.Status,
                               p.Id,
                               p.Name, 
                               p.Price,
                               SUM(cp.Quantity) AS Quantity,
                               p.MainImageUrl,
                               p.Discount,
                               p.Tags
                           FROM Orders o
                           INNER JOIN CustomerProducts cp ON o.Id = cp.OrderId
                           INNER JOIN Products p ON cp.ProductId = p.Id 
                           WHERE o.Id = {orderId} AND o.Status = {(long)OrderTypes.Initialized}
                           GROUP BY cp.ProductId;";

            var result = await con.QueryAsync<OrderDTO, ProductDTO, OrderDTO>(query, (order, product) =>
            {
                order.Products ??= new List<ProductDTO>();

                order.Products.Add(product);

                return order;
            }, splitOn: "Status");

            var orders = result.GroupBy(o => o.Id).Select(g =>
            {
                var order = g.First();

                order.Amount = g.Sum(o => o.Amount);

                order.Products = g.SelectMany(o => o.Products).ToList();

                return order;
            });

            return orders.FirstOrDefault();
        }

        public async Task<IEnumerable<OrderDTO>> GetAllByCustomerId(long customerId)
        {
            using var con = _factory.GetDbConnection;

            var query = @$"SELECT 
                               o.Id, 
                               o.OrganizationId, 
                               o.CustomerId, 
                               o.PaymentMethodId,
                               o.Amount, 
                               o.Items, 
                               o.Discount,
                               o.Installments,
                               o.DeliveryType,
                               o.Status,
                               p.Id,
                               p.Name, 
                               p.Price,
                               SUM(cp.Quantity) AS Quantity,
                               p.MainImageUrl,
                               p.Discount,
                               p.Tags
                           FROM Orders o
                           INNER JOIN CustomerProducts cp ON o.Id = cp.OrderId
                           INNER JOIN Products p ON cp.ProductId = p.Id 
                           WHERE o.CustomerId = {customerId}
                           GROUP BY cp.ProductId;";

            var result = await con.QueryAsync<OrderDTO, ProductDTO, OrderDTO>(query, (order, product) =>
            {
                order.Products ??= new List<ProductDTO>();

                order.Products.Add(product);

                return order;
            }, splitOn: "Status");

            var orders = result.GroupBy(o => o.Id).Select(g =>
            {
                var order = g.First();

                order.Amount = g.Sum(o => o.Amount);

                order.Products = g.SelectMany(o => o.Products).ToList();

                return order;
            });

            return orders;
        }

        public async Task<ServiceResponse> PostCreateOrder(CreateOrderDTO request, long userId)
        {
            using var con = _factory.GetDbConnection;

            var query = @$"INSERT INTO Orders (OrganizationId,
                                               CustomerId, 
                                               PaymentMethodId, 
                                               Amount, 
                                               Items,
                                               Discount, 
                                               Installments, 
                                               DeliveryType, 
                                               Status, 
                                               CreatedBy,
                                               CreatedDate)
                                        VALUES(@OrganizationId, 
                                               @CustomerId, 
                                               @PaymentMethodId, 
                                               @Amount,
                                               @Items, 
                                               @Discount, 
                                               @Installments,
                                               @DeliveryType, 
                                               {(long)OrderTypes.Initialized},
                                               {userId}, 
                                               CURRENT_TIMESTAMP);
                           SELECT MAX(Id) FROM Orders;";

            var orderId = await con.QuerySingleAsync<long>(query, request);

            query = $@"INSERT INTO CustomerProducts (CustomerId,
                                                     OrderId,
                                                     ProductId, 
                                                     Quantity, 
                                                     Price, 
                                                     Discount, 
                                                     CreatedBy, 
                                                     CreatedDate)
                                              VALUES(@CustomerId,
                                                     @OrderId,
                                                     @ProductId,
                                                     @Quantity, 
                                                     @Price,
                                                     @Discount, 
                                                     @CreatedBy,
                                                     CURRENT_TIMESTAMP);
                       SELECT MAX(Id) FROM CustomerProducts;";

            var customerProductId = await con.QuerySingleAsync<long>(query, new
            {
                request.CustomerId,
                OrderId = orderId,
                ProductId = request.Product.Id,
                request.Product.Quantity,
                request.Product.Price,
                Discount = 0,
                CreatedBy = userId,
            });

            return new ServiceResponse
            {
                Metadata = new Metadata
                {
                    Message = "Success",
                    Status = System.Net.HttpStatusCode.OK
                },
                Data = new Data
                {
                    Id = orderId,
                    NewId = customerProductId
                }
            };
        }

        public async Task<ServiceResponse> PutAddItemsToOrder(CreateOrderDTO request, long userId)
        {
            using var con = _factory.GetDbConnection;

            var query = @$"UPDATE Orders SET Items = {request.Items}, ModifiedBy = {userId}, ModifiedDate = CURRENT_TIMESTAMP, Amount = {request.Amount},
                           PaymentMethodId = {request.PaymentMethodId}
                           WHERE Id = {request.Id} AND Status = {(long)OrderTypes.Initialized};";

            var result = await con.ExecuteAsync(query);

            query = $@"INSERT INTO CustomerProducts (CustomerId,
                                                     OrderId,
                                                     ProductId, 
                                                     Quantity, 
                                                     Price, 
                                                     Discount, 
                                                     CreatedBy, 
                                                     CreatedDate)
                                              VALUES(@CustomerId,
                                                     @OrderId,
                                                     @ProductId,
                                                     @Quantity, 
                                                     @Price,
                                                     @Discount, 
                                                     @CreatedBy,
                                                     CURRENT_TIMESTAMP);
                       SELECT MAX(Id) FROM CustomerProducts;";

            var customerProductId = await con.QuerySingleAsync<long>(query, new
            {
                request.CustomerId,
                OrderId = request.Id,
                ProductId = request.Product.Id,
                request.Product.Quantity,
                request.Product.Price,
                Discount = 0,
                CreatedBy = userId,
            });

            return new ServiceResponse
            {
                Metadata = new Metadata
                {
                    Message = "Success",
                    Status = System.Net.HttpStatusCode.OK
                },
                Data = new Data
                {
                    Id = request.Id,
                    NewId = customerProductId
                }
            };
        }

        public async Task<ServiceResponse> DeleteOrderItem(RemoveOrderItemDTO request, long userId)
        {
            using var con = _factory.GetDbConnection;

            var query = @$"UPDATE Orders SET Items = {request.Items}, ModifiedBy = {userId}, ModifiedDate = CURRENT_TIMESTAMP, Amount = {request.Amount}
                           WHERE Id = {request.Id} AND Status = {(long)OrderTypes.Initialized};";

            var result = await con.ExecuteAsync(query);

            query = $@"DELETE FROM CustomerProducts WHERE ProductId = {request.ProductId};";

            await con.ExecuteAsync(query);

            return new ServiceResponse
            {
                Metadata = new Metadata
                {
                    Message = "Success",
                    Status = System.Net.HttpStatusCode.OK
                },
                Data = new Data
                {
                    Id = request.Id,
                }
            };
        }

        public async Task<ServiceResponse> DeleteOrder(long orderId, long organizationId)
        {
            using var con = _factory.GetDbConnection;

            var query = $@"DELETE FROM CustomerProducts WHERE OrderId = {orderId};
                           DELETE FROM Orders WHERE Id = {orderId};";

            await con.ExecuteAsync(query);

            return new ServiceResponse
            {
                Metadata = new Metadata
                {
                    Message = "Success",
                    Status = System.Net.HttpStatusCode.OK
                }
            };
        }

        public async Task<ServiceResponse> PutFinishOrder(UpdateOrderDTO request, long orderId, long userId)
        {
            using var con = _factory.GetDbConnection;

            var query = @$"UPDATE Orders 
                           SET
                               PaymentMethodId = @PaymentMethodId,
                               Amount = @Amount,
                               Discount = @Discount,
                               Items = @Items,
                               Installments = @Installments,
                               DeliveryType = @DeliveryType,
                               Status = {OrderTypes.Finished},
                               ModifiedBy = {userId},
                               ModifiedDate = CURRENT_TIMESTAMP 
                           WHERE Id = {orderId};";

            var result = await con.ExecuteAsync(query, new
            {
                request.PaymentMethodId,
                request.Amount,
                request.Discount,
                request.Items,
                request.Installments,
                request.DeliveryType
            });

            return new ServiceResponse
            {
                Metadata = new Metadata
                {
                    Message = "Success",
                    Status = System.Net.HttpStatusCode.OK
                },
                Data = new Data
                {
                    Id = result,
                }
            };
        }
    }
}