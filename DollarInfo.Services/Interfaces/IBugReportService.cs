using DollarInfo.DAL.Models;

namespace DollarInfo.Services.Interfaces
{
    public interface IBugReportService
    {
        Task Post(BugReportRequest request);
    }
}