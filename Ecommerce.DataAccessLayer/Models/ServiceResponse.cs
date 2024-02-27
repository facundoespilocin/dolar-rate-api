using System.Net;

namespace Ecommerce.DataAccessLayer.Models
{
    public class ServiceResponse
    {
        public Metadata Metadata { get; set; }
        public Data Data { get; set; }

        public ServiceResponse()
        {
            Metadata = new Metadata();
            Data = new Data();
        }
    }

    public class Metadata
    {
        public HttpStatusCode Status { get; set; }
        public string? Message { get; set; }
        public string? Next { get; set; }
    }

    public class Data
    {
        public long Id { get; set; }
        public long NewId { get; set; }
        public string? Name { get; set; }
        public long Count { get; set; }
    }
}