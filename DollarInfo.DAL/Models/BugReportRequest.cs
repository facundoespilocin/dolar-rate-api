namespace DollarInfo.DAL.Models
{
    public class BugReportRequest
    {
        public BugReportData BugReport { get; set; }

        public class BugReportData
        {
            public string Name { get; set; }
            public string EmailFrom { get; set; }
            public string Description { get; set; }
        }
    }
}