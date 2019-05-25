using erm.CsvProcessor.interfaces;
using erm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace erm.CsvProcessor
{
    public class MedianProcessor : IMedianProcessor
    {
        public Result<MedianResult> GetMedianResult(List<EnergyRecord> records)
        {
            if (records == null || records.Count() < 1)
                return new Result<MedianResult>(false, null, "No records to process median");

            var count = records.Count();

            var ordered = records.OrderBy(x => x.DataValue);
            var median = ordered
                .Skip(count / 2)
                .First();

            var result = ordered.Skip((int)(count * 0.3))
                .Take((int)(count * 0.4))
                .ToList();

            return new Result<MedianResult>(true, new MedianResult { Median = median.DataValue, EnergyRecords = result }, null);
        }
    }
}
