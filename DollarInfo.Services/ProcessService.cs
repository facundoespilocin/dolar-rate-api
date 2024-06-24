using AutoMapper;
using DollarInfo.DAL.Dtos.Indexes;
using DollarInfo.DAL.Repositories.Interfaces;
using DollarInfo.Services.Interfaces;
using DollarInfo.Utils;
using DollarInfo.Utils.Extensions;
using static DollarInfo.Utils.Enums;

namespace DollarInfo.Services
{
    public class ProcessesService : IProcessesService
    {
        private readonly IMapper _mapper;
        private readonly IRatesService _ratesService;
        private readonly IIndexesService _indexesService;
        private readonly IProcessesRepository _processesRepository;

        public ProcessesService(
            IMapper mapper,
            IRatesService ratesService,
            IIndexesService indexesService,
            IProcessesRepository processesRepository)
        {
            _mapper = mapper;
            _ratesService = ratesService;
            _indexesService = indexesService;
            _processesRepository = processesRepository;
        }

        public async Task PostInflationIndex(InflationIndexTypes inflationIndexType, InsertionTypes insertionType)
        {
            try
            {
                var inflationIndexes = await _indexesService.GetInflationIndexes(inflationIndexType);

                if (inflationIndexes.Data?.Any() ?? false)
                {
                    if (insertionType == InsertionTypes.Bulk)
                    {
                        await _processesRepository.InsertBulkInflationIndex(inflationIndexes.Data, inflationIndexType);
                    }
                    else
                    {
                        var lastInflationIndex = inflationIndexes.Data.LastOrDefault();

                        var lastDate = lastInflationIndex.Date.ParseDateExact(Constants.DateFormats.Dash.Default);

                        var lastInflationIndexRecord = await _processesRepository.GetLastInflationIndex(inflationIndexType);

                        var lastDateRecord = lastInflationIndexRecord.Date.ParseDateExact(Constants.DateFormats.Slashes.MonthFirst);

                        if (lastDate > lastDateRecord)
                        {
                            await _processesRepository.InsertInflationIndex(lastInflationIndex, inflationIndexType);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting Inflation Indexes: {ex.Message}");
            }
        }
    }
}