using DollarInfo.DAL.Dtos.Indexes;
using static DollarInfo.Utils.Enums;

namespace DollarInfo.DAL.Repositories.Interfaces
{
    public interface IProcessesRepository
    {
        Task<InflationIndexDto> GetLastInflationIndex(InflationIndexTypes inflationIndexType);
        Task InsertInflationIndex(InflationIndexDto inflationIndex, InflationIndexTypes inflationIndexType);
        Task InsertBulkInflationIndex(IEnumerable<InflationIndexDto> inflationIndex, InflationIndexTypes inflationIndexType);
    }
}