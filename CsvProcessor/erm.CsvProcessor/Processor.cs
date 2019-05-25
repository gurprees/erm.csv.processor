using erm.CsvProcessor.interfaces;
using Microsoft.Extensions.Logging;
using System.IO;

namespace erm.CsvProcessor
{
    public class Processor : IProcessor
    {
        private readonly ILogger<Processor> _logger;
        private readonly ICsvParser _csvParser;
        private readonly IMedianProcessor _medianProcessor;
        public Processor(
            ILogger<Processor> logger,
            ICsvParser csvParser, 
            IMedianProcessor medianProcessor)
        {
            _logger = logger;
            _csvParser = csvParser;
            _medianProcessor = medianProcessor;
        }

        public void ProcessFiles(FileInfo fileInfo)
        { 
            //Get parsed csv data
            var dataResult = _csvParser.GetData(fileInfo);
            if (!dataResult.Success)
            {
                _logger.LogError(dataResult.ErrorMessage);
                return;
            }

            //Get median data
            var medianResult = _medianProcessor.GetMedianResult(dataResult.Value);
            if (!medianResult.Success)
            {
                _logger.LogError(medianResult.ErrorMessage);
                return;
            }

            //Log the result
            medianResult.Value.EnergyRecords.ForEach(x =>
                _logger.LogInformation($"{fileInfo.Name} {x.DateTime} {x.DataValue} {medianResult.Value.Median}"));
        }
    }

}
