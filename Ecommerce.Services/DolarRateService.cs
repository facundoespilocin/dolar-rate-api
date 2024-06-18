using AutoMapper;
using Ecommerce.DataAccessLayer.Entities.Misc;
using Ecommerce.DataAccessLayer.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Utils.Settings;
using Microsoft.Extensions.Options;

namespace Ecommerce.Services
{
    public class DolarRateService : IDolarRateService
    {
        private readonly IMiscRepository _miscRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public DolarRateService(IMiscRepository miscRepository, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _miscRepository = miscRepository;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DropdownItem>> GetAllExchangeRates()
        {
            return await _miscRepository.GetCountries();
        }
    }
}