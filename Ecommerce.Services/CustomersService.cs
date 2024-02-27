using AutoMapper;
using Ecommerce.DataAccessLayer.Entities.Product;
using Ecommerce.DataAccessLayer.Models;
using Ecommerce.DataAccessLayer.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Utils.Settings;
using Microsoft.Extensions.Options;

namespace Ecommerce.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public CustomersService(ICustomersRepository customersRepository, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _customersRepository = customersRepository;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Customer>> GetAll(SearchRequest request)
        {
            var customers = await _customersRepository.GetAll(request);

            return customers;
        }
    }
}