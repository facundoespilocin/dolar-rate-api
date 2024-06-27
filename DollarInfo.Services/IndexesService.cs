using AutoMapper;
using DollarInfo.DAL.Dtos.Indexes;
using DollarInfo.DAL.Models;
using DollarInfo.DAL.Repositories.Interfaces;
using DollarInfo.Services.Factories;
using DollarInfo.Services.Helpers;
using DollarInfo.Services.Interfaces;
using DollarInfo.Services.Models;
using static DollarInfo.Utils.Enums;

namespace DollarInfo.Services
{
    public class IndexesService : IIndexesService
    {
        private readonly HttpClientWrapper _httpClientWrapper;
        private readonly IndexUrlFactory _indexUrlFactory;
        private readonly IMapper _mapper;
        private readonly IProcessesRepository _processesRepository;

        public IndexesService(
            HttpClientWrapper httpClientWrapper,
            IndexUrlFactory indexUrlFactory,
            IMapper mapper,
            IProcessesRepository processesRepository)
        {
            _httpClientWrapper = httpClientWrapper;
            _mapper = mapper;
            _indexUrlFactory = indexUrlFactory;
            _processesRepository = processesRepository;
        }

        public async Task<ServiceResponse<IEnumerable<InflationIndexDto>>> GetAll(InflationIndexTypes inflationIndexType)
        {
            ServiceResponse<IEnumerable<InflationIndexDto>> serviceResponse = new();

            try
            {
                serviceResponse.Data = await _processesRepository.GetAll(inflationIndexType);
            }
            catch (Exception ex)
            {
                serviceResponse.AddError(ex);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<InflationIndexDto>>> GetInflationIndexes(InflationIndexTypes inflationIndexType)
        {
            string url = _indexUrlFactory.GetIndexUrl(inflationIndexType);

            ServiceResponse<IEnumerable<InflationIndexDto>> serviceResponse = new();

            try
            {
                var response = await _httpClientWrapper.GetAsync<IEnumerable<InflationIndexResponse>>(url);

                serviceResponse.Data = _mapper.Map<IEnumerable<InflationIndexDto>>(response.Data);
            }
            catch (Exception ex)
            {
                serviceResponse.AddError(ex);
            }

            return serviceResponse;
        }
    }
}