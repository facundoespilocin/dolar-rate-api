using Dapper;
using DollarInfo.DAL.Factory;
using DollarInfo.DAL.Repositories.Interfaces;
using DollarInfo.Services.Models;

namespace DollarInfo.DAL.Repositories
{
    public class ProcessesRepository : IProcessesRepository
    {
        private IConnectionFactory _factory;

        public ProcessesRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task InsertInflationIndex(IEnumerable<DollarRatesResponse> dollarRates)
        {
            using var con = _factory.GetDbConnection;

            var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var query = @$"INSERT INTO ExchangeRates (Name, IsActive, CategoryId, Price, Quantity, BrandId, Tags, ImageUrl, CreatedBy, CreatedDate)
                                        VALUES(@Name, @IsActive, @CategoryId, @Price, @Quantity, @BrandId, @Tags, @ImageUrl, 1, '{currentDate}');";

            var result = await con.ExecuteAsync(query, dollarRates);

            //return new ServiceResponse
            //{
            //    Metadata = new Metadata
            //    {
            //        Message = "Success",
            //        Status = System.Net.HttpStatusCode.OK
            //    },
            //    Data = new Data
            //    {
            //        Id = result,
            //    }
            //};

            //return await con.QueryAsync<DropdownItem>(query);
        }
    }
}