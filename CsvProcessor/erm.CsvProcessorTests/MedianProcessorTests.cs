using erm.CsvProcessor;
using erm.Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class MedianProcessorTests
    {
        private List<EnergyRecord> records;

        [Test]
        public void When_data_available_return_median_data()
        {
            var medianProcessor = new MedianProcessor();
            var result = medianProcessor.GetMedianResult(records);

            var energyRecords = result.Value.EnergyRecords;
            Assert.AreEqual(45.0m, result.Value.Median);
            Assert.AreEqual(6, energyRecords.Count);
            Assert.AreEqual(25.0m, energyRecords[0].DataValue);
            Assert.AreEqual(35.0m, energyRecords[1].DataValue);
            Assert.AreEqual(40.0m, energyRecords[2].DataValue);
            Assert.AreEqual(45.0m, energyRecords[3].DataValue);
            Assert.AreEqual(75.0m, energyRecords[4].DataValue);
            Assert.AreEqual(75.0m, energyRecords[5].DataValue);

        }

        [Test]
        public void When_data_null_return_false()
        {
            var medianProcessor = new MedianProcessor();
            var result = medianProcessor.GetMedianResult(null);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("No records to process median", result.ErrorMessage);
        }

        [Test]
        public void When_data_count_zero_return_false()
        {
            var medianProcessor = new MedianProcessor();
            var result = medianProcessor.GetMedianResult(new List<EnergyRecord>());

            Assert.IsFalse(result.Success);
            Assert.AreEqual("No records to process median", result.ErrorMessage);
        }

        [SetUp]
        public void Setup()
        {
            records = new List<EnergyRecord>();
            records.Add(new EnergyRecord { DataValue = 20.0m, DateTime = "30/12/1899 01:00:00" });
            records.Add(new EnergyRecord { DataValue = 35.0m, DateTime = "30/12/1899 02:00:00" });
            records.Add(new EnergyRecord { DataValue = 10.0m, DateTime = "30/12/1899 03:00:00" });
            records.Add(new EnergyRecord { DataValue = 40.0m, DateTime = "30/12/1899 04:00:00" });
            records.Add(new EnergyRecord { DataValue = 80.0m, DateTime = "30/12/1899 05:00:00" });
            records.Add(new EnergyRecord { DataValue = 45.0m, DateTime = "30/12/1899 06:00:00" });
            records.Add(new EnergyRecord { DataValue = 75.0m, DateTime = "30/12/1899 07:00:00" });
            records.Add(new EnergyRecord { DataValue = 15.0m, DateTime = "30/12/1899 08:00:00" });
            records.Add(new EnergyRecord { DataValue = 25.0m, DateTime = "30/12/1899 09:00:00" });
            records.Add(new EnergyRecord { DataValue = 80.0m, DateTime = "30/12/1899 10:00:00" });
            records.Add(new EnergyRecord { DataValue = 75.0m, DateTime = "30/12/1899 11:00:00" });
            records.Add(new EnergyRecord { DataValue = 15.0m, DateTime = "30/12/1899 12:00:00" });
            records.Add(new EnergyRecord { DataValue = 95.0m, DateTime = "30/12/1899 13:00:00" });
            records.Add(new EnergyRecord { DataValue = 90.0m, DateTime = "30/12/1899 14:00:00" });
            records.Add(new EnergyRecord { DataValue = 15.0m, DateTime = "30/12/1899 15:00:00" });
            records.Add(new EnergyRecord { DataValue = 95.0m, DateTime = "30/12/1899 16:00:00" });
            records.Add(new EnergyRecord { DataValue = 90.0m, DateTime = "30/12/1899 17:00:00" });
        }
    }
}