using Dapper;
using Ecommerce.DataAccessLayer.Entities.Category;
using Ecommerce.DataAccessLayer.Factory;
using Ecommerce.DataAccessLayer.Models;
using Ecommerce.DataAccessLayer.Repositories.Interfaces;

namespace Ecommerce.DataAccessLayer.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private IConnectionFactory _factory;

        public CategoriesRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Category>> GetAll(SearchRequest request)
        {
            using var con = _factory.GetDbConnection;

            List<string> whereClause = new() { };

            if (request.Status != Utils.Enums.StatusTypes.All)
            {
                whereClause.Add($"c.IsActive = {(int)request.Status}");
            }

            if (!string.IsNullOrEmpty(request.Query) && !string.IsNullOrEmpty(request.SearchBy))
            {
                whereClause.Add($"UPPER({request.SearchBy}) LIKE UPPER('%{request.Query}%')");
            }

            var where = (whereClause.Any() ? " WHERE " : "") + string.Join(" AND ", whereClause);

            string select = "SELECT * FROM Categories c ";

            var productsQuery = $@"{select} {where};";

            var categories = await con.QueryAsync<Category>(productsQuery);

            return categories;
        }

        public async Task<Category> GetById(long categoryId)
        {
            using var con = _factory.GetDbConnection;

            var query = $"SELECT * FROM Categories WHERE Id = {categoryId};";

            var user = await con.QueryFirstOrDefaultAsync<Category>(query);

            return user;
        }

        public async Task<ServiceResponse> InsertCategories(IEnumerable<Category> categories)
        {
            using var con = _factory.GetDbConnection;

            var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var query = @$"INSERT INTO Categories (Name, HierarchicalName, Level, HasChildren, IsActive, ParentId, Ancestry, CreatedBy, CreatedDate)
                                           VALUES(@Name, @HierarchicalName, @Level, @HasChildren, @IsActive, @ParentId, @Ancestry, 1, '{currentDate}');";

            var result = await con.ExecuteAsync(query, categories);

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

        public async Task<ServiceResponse> InsertCategory(CreateCategoryDTO request)
        {
            using var con = _factory.GetDbConnection;

            var childrenQuery = string.Empty;

            var query = @$"INSERT INTO Categories (Name, HierarchicalName, Level, HasChildren, IsActive, ParentId, CreatedBy, CreatedDate)
                                           VALUES(@Name, @HierarchicalName, @Level, @HasChildren, @IsActive, @ParentId, @CreatedBy, @CreatedDate);
                           SELECT LAST_INSERT_ID();";

            var result = await con.QuerySingleOrDefaultAsync<long>(query, new
            {
                request.Category.Name,
                request.Category.HierarchicalName,
                request.Category.Level,
                request.Category.HasChildren,
                request.Category.IsActive,
                request.Category.ParentId,
                CreatedBy = 1,
                request.Category.CreatedDate
            });

            if (request.Category.HasChildren)
            {
                foreach (var children in request.Category.Children)
                {
                    await con.ExecuteAsync(query, new
                    {
                        children.Name,
                        HierarchicalName = request.Category.HierarchicalName + " - " + children.Name,
                        Level = request.Category.Level + 1,
                        children.HasChildren,
                        request.Category.IsActive,
                        ParentId = result,
                        CreatedBy = 1,
                        request.Category.CreatedDate
                    });
                }
            }

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