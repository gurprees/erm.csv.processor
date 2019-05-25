using erm.CsvProcessor;
using erm.CsvProcessor.interfaces;
using erm.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace erm.CsvProcessorTests
{
    public class ProcessorTests
    {
        private Mock<ILogger<Processor>> _loggerMock;
        private Mock<ICsvParser> _csvParserMock;
        private List<EnergyRecord> records;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Processor>>();
            _csvParserMock = new Mock<ICsvParser>();
            _medianProcessorMock = new Mock<IMedianProcessor>();

            records = new List<EnergyRecord>();
            records.Add(new EnergyRecord { DataValue = 20.0m, DateTime = "30/12/1899 01:00:00" });
            records.Add(new EnergyRecord { DataValue = 35.0m, DateTime = "30/12/1899 02:00:00" });
            records.Add(new EnergyRecord { DataValue = 10.0m, DateTime = "30/12/1899 03:00:00" });
            records.Add(new EnergyRecord { DataValue = 40.0m, DateTime = "30/12/1899 04:00:00" });
            records.Add(new EnergyRecord { DataValue = 80.0m, DateTime = "30/12/1899 05:00:00" });
            records.Add(new EnergyRecord { DataValue = 45.0m, DateTime = "30/12/1899 06:00:00" });
            records.Add(new EnergyRecord { DataValue = 75.0m, DateTime = "30/12/1899 07:00:00" });
            records.Add(new EnergyRecord { DataValue = 15.0m, DateTime = "30/12/1899 08:00:00" });
        }

        [Test]
        public void When_file_available_log_median_results()
        {
            var result = new Result<List<EnergyRecord>>(true, records, null);
            _csvParserMock.Setup(x => x.GetData(It.IsAny<FileInfo>())).Returns(result);

            var processor = new Processor(_loggerMock.Object, _csvParserMock.Object, new MedianProcessor());
            processor.ProcessFiles(new FileInfo("somfile.csv"));

            _loggerMock.Verify(x => x.Log(LogLevel.Information, 
                It.IsAny<EventId>(), 
                It.IsAny<FormattedLogValues>(), 
                It.IsAny<Exception>(), 
                It.IsAny<Func<object, Exception, string>>()), 
                Times.Exactly(3));
        }

        [Test]
        public void When_invalid_file_available_log_error()
        {
            var result = new Result<List<EnergyRecord>>(false, null, "some error message");
            _csvParserMock.Setup(x => x.GetData(It.IsAny<FileInfo>())).Returns(result);

            var processor = new Processor(_loggerMock.Object, _csvParserMock.Object, new MedianProcessor());
            processor.ProcessFiles(new FileInfo("somfile.csv"));

            _loggerMock.Verify(x => x.Log(LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<FormattedLogValues>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception, string>>()),
                Times.Once);
        }
    }
}
