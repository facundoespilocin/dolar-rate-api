using Dapper;
using Ecommerce.DataAccessLayer.Entities.Product;
using Ecommerce.DataAccessLayer.Entities.User;
using Ecommerce.DataAccessLayer.Factory;
using Ecommerce.DataAccessLayer.Models;
using Ecommerce.DataAccessLayer.Repositories.Interfaces;
using System.Linq;

namespace Ecommerce.DataAccessLayer.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private IConnectionFactory _factory;

        public ProductsRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<ProductDTO>> GetAll(SearchRequest request)
        {
            using var con = _factory.GetDbConnection;

            List<string> whereClause = new() { };

            if (request.Status != Utils.Enums.StatusTypes.All)
            {
                whereClause.Add($"p.IsActive = {(int)request.Status}");
            }

            if (!string.IsNullOrEmpty(request.Query) && !string.IsNullOrEmpty(request.SearchBy))
            {
                whereClause.Add($"UPPER({request.SearchBy}) LIKE UPPER('%{request.Query}%')");
            }

            var where = (whereClause.Any() ? " WHERE " : "") + string.Join(" AND ", whereClause);

            string offset = $" OFFSET {request.PageSize * (request.CurrentPage - 1)}";

            string limit = $" LIMIT {request.PageSize}";

            string query = $@"SELECT 
                               p.Id, 
                               p.Name, 
                               p.Description, 
                               p.ShortDescription, 
                               p.IsActive, 
                               p.Price, 
                               p.Quantity, 
                               p.Sizes AS SizesList,
                               p.CategoryId, 
                               p.BrandId, 
                               p.PaymentMethods AS PaymentMethodsList, 
                               p.DeliveriesIds, 
                               p.MainImageUrl, 
                               p.ImagesUrl AS ImagesUrlList, 
                               p.Discount, 
                               p.CustomAttributes, 
                               p.Tags, 
                               p.CreatedBy, 
                               p.CreatedDate,
                               MAX(CASE WHEN d.Type = 1 THEN d.ShippingCost END) AS ShippingCost,
                               MAX(CASE WHEN d.Type = 2 THEN d.ShippingCost END) AS WithdrawalCost,
                               c.Name AS CategoryName 
                           FROM Products p
                           LEFT JOIN Deliveries d ON FIND_IN_SET(d.Type, p.DeliveriesIds)
                           INNER JOIN Categories c ON p.CategoryId = c.Id
                           GROUP BY p.Id, p.Name";

            var productsQuery = $@"{query} {where} {limit} {offset};";

            var products = await con.QueryAsync<ProductDTO>(productsQuery);

            return products;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllFiltered(SearchRequest request)
        {
            using var con = _factory.GetDbConnection;

            List<string> whereClause = new() { };

            if (!string.IsNullOrEmpty(request.Query) && !string.IsNullOrEmpty(request.SearchBy))
            {
                whereClause.Add($"UPPER({request.SearchBy}) LIKE UPPER('%{request.Query}%')");
            }

            var where = (whereClause.Any() ? " WHERE " : "") + string.Join(" AND ", whereClause);

            //string offset = request.CurrentPage > 1 ? $" OFFSET {request.PageSize * (request.CurrentPage - 1)}" : "";

            //string limit = $" LIMIT {request.PageSize} ";

            //string orderBy = !string.IsNullOrWhiteSpace(filter.FieldName) ? $" ORDER BY {filter.FieldName} {filter.OrderType}" : $" ORDER BY TRIM(u.FullName) ";

            string select = "SELECT * FROM Products ";

            var productsQuery = $@"{select} {where};";

            //var totalsQuery = $@"SELECT DISTINCT(COUNT(*)) FROM Products;";

            //var totals = await con.QueryFirstOrDefaultAsync<long>(totalsQuery);

            var products = await con.QueryAsync<ProductDTO>(productsQuery);

            return products;
        }

        public async Task<ProductDetailDTO> GetById(long productId)
        {
            using var con = _factory.GetDbConnection;

            var query = $@"SELECT 
                               p.Id, 
                               p.Name, 
                               p.Description, 
                               p.ShortDescription, 
                               p.IsActive, 
                               p.Price, 
                               p.Quantity, 
                               p.Sizes AS SizesList,
                               p.CategoryId, 
                               p.BrandId, 
                               p.PaymentMethods AS PaymentMethodsList, 
                               p.DeliveriesIds, 
                               p.MainImageUrl, 
                               p.ImagesUrl AS ImagesUrlList, 
                               p.Discount, 
                               p.CustomAttributes, 
                               p.Tags, 
                               p.CreatedBy, 
                               p.CreatedDate,
                               MAX(CASE WHEN d.Type = 1 THEN d.ShippingCost END) AS ShippingCost,
                               MAX(CASE WHEN d.Type = 2 THEN d.ShippingCost END) AS WithdrawalCost
                           FROM Products p
                           LEFT JOIN Deliveries d ON FIND_IN_SET(d.Type, p.DeliveriesIds)
                           WHERE p.Id = {productId}
                           GROUP BY p.Id, p.Name;";

            var product = await con.QueryFirstOrDefaultAsync<ProductDetailDTO>(query);

            if (!string.IsNullOrEmpty(product.SizesList))
            {
                product.Sizes = product.SizesList.Split(',').Select(s => s.Trim());
            }

            if (!string.IsNullOrEmpty(product.PaymentMethodsList))
            {
                product.PaymentMethods = product.PaymentMethodsList.Split(',').Select(s => s.Trim());
            }

            if (!string.IsNullOrEmpty(product.DeliveriesIds))
            {
                product.Deliveries = product.DeliveriesIds.Split(',').Select(s => s.Trim());
            }
            
            if (!string.IsNullOrEmpty(product.ImagesUrlList))
            {
                product.ImagesUrl = product.ImagesUrlList.Split(',').Select(s => s.Trim());
            }

            return product;
        }

        public async Task<IEnumerable<ProductDetailDTO>> GetByIds(long productId)
        {
            using var con = _factory.GetDbConnection;

            var query = $@"
        SELECT 
            p.Id, 
            p.Name, 
            p.Description, 
            ps.Size AS Sizes,
            pp.PaymentMethod AS PaymentMethods,
            pd.Delivery AS Deliveries,
            pi.ImageUrl AS ImagesUrl
        FROM Products p
        OUTER APPLY STRING_SPLIT(p.Sizes, ',') ps
        OUTER APPLY STRING_SPLIT(p.PaymentMethods, ',') pp
        OUTER APPLY STRING_SPLIT(p.DeliveriesIds, ',') pd
        OUTER APPLY STRING_SPLIT(p.ImagesUrl, ',') pi
        WHERE p.Id = {productId};
    ";

            var result = await con.QueryAsync<ProductDetailDTO>(query);

            //if (!string.IsNullOrEmpty(product.SizesList))
            //{
            //    product.Sizes = product.SizesList.Split(',').Select(s => s.Trim());
            //}

            //if (!string.IsNullOrEmpty(product.PaymentMethodsList))
            //{
            //    product.PaymentMethods = product.PaymentMethodsList.Split(',').Select(s => s.Trim());
            //}

            //if (!string.IsNullOrEmpty(product.DeliveriesIds))
            //{
            //    product.Deliveries = product.DeliveriesIds.Split(',').Select(s => s.Trim());
            //}

            //if (!string.IsNullOrEmpty(product.ImagesUrlList))
            //{
            //    product.ImagesUrl = product.ImagesUrlList.Split(',').Select(s => s.Trim());
            //}

            return result;
        }

        public async Task<ServiceResponse> InsertProducts(IEnumerable<Product> products)
        {
            using var con = _factory.GetDbConnection;

            var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var query = @$"INSERT INTO Products (Name, IsActive, CategoryId, Price, Quantity, BrandId, Tags, ImageUrl, CreatedBy, CreatedDate)
                                        VALUES(@Name, @IsActive, @CategoryId, @Price, @Quantity, @BrandId, @Tags, @ImageUrl, 1, '{currentDate}');";

            var result = await con.ExecuteAsync(query, products);

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

        public async Task<ServiceResponse> InsertProduct(ProductDTO request)
        {
            using var con = _factory.GetDbConnection;

            var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var query = @$"INSERT INTO Products (Name, Description, ShortDescription, IsActive, CategoryId, Price, Quantity, Discount, CustomAttributes, ImageUrl, CreatedBy, CreatedDate)
                                         VALUES(@Name, @Description, @ShortDescription, @IsActive, @CategoryId, @Price, @Quantity, @Discount, @CustomAttributes, @ImageUrl, 1, '{currentDate}');";

            var result = await con.ExecuteAsync(query, request);

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

        public async Task Update(User user)
        {
            using var con = _factory.GetDbConnection;

            var query = @"UPDATE Users SET Name=@Name, Height=@Height, Mass=@Mass, HairColor=@HairColor, 
                        SkinColor=@SkinColor, EyeColor=@EyeColor, BirthYear=@BirthYear, Gender=@Gender, 
                        Homeworld=@Homeworld, Created=@Created, Edited=@Edited, Url=@Url
                        WHERE Id=@Id";

            await con.ExecuteAsync(query, user);
        }

        public async Task Delete(int productId)
        {
            using var con = _factory.GetDbConnection;

            var query = $@"DELETE FROM Products WHERE Id = {productId}";

            await con.ExecuteAsync(query);
        }
    }
}