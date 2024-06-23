using DollarInfo.DAL.Dtos.Indexes;
using DollarInfo.DAL.Dtos.Rates;
using DollarInfo.DAL.Models;
using static DollarInfo.Utils.Enums;

namespace DollarInfo.Services.Interfaces
{
    public interface IIndexesService
    {
        Task<ServiceResponse<IEnumerable<InflationIndexDto>>> GetInflationIndexes(InflationIndexTypes inflationIndexType);
    }
}