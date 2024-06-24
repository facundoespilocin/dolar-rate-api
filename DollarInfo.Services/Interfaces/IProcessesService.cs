using DollarInfo.DAL.Models;
using static DollarInfo.Utils.Enums;

namespace DollarInfo.Services.Interfaces
{
    public interface IProcessesService
    {
        Task PostInflationIndex(InflationIndexTypes inflationIndexType);
    }
}