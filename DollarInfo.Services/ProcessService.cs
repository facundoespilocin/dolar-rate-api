using AutoMapper;
using DollarInfo.Services.Interfaces;
using static DollarInfo.Utils.Enums;

namespace DollarInfo.Services
{
    public class ProcessesService : IProcessesService
    {
        private readonly IMapper _mapper;
        private readonly IRatesService _ratesService;
        private readonly IIndexesService _indexesService;

        public ProcessesService(
            IMapper mapper,
            IRatesService ratesService,
            IIndexesService indexesService)
        {
            _mapper = mapper;
            _ratesService = ratesService;
            _indexesService = indexesService;
        }

        public async Task PostInflationIndex(InflationIndexTypes inflationIndexType)
        {
            var result = _indexesService.GetInflationIndexes(inflationIndexType);
        }
    }
}