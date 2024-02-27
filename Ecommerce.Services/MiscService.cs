using AutoMapper;
using Ecommerce.DataAccessLayer.Entities.Misc;
using Ecommerce.DataAccessLayer.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Utils.Settings;
using Microsoft.Extensions.Options;

namespace Ecommerce.Services
{
    public class MiscService : IMiscService
    {
        private readonly IMiscRepository _miscRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public MiscService(IMiscRepository miscRepository, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _miscRepository = miscRepository;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DropdownItem>> GetCountries()
        {
            return await _miscRepository.GetCountries();
        }

        public async Task<DropdownItem> GetCountryById(long countryId)
        { 
            return await _miscRepository.GetCountryById(countryId);;
        }

        public async Task<IEnumerable<DropdownItem>> GetIndustries()
        {
            return await _miscRepository.GetIndustries();
        }

        public async Task<DropdownItem> GetIndustryById(long countryId)
        {
            return await _miscRepository.GetIndustryById(countryId); ;
        }
    }
}