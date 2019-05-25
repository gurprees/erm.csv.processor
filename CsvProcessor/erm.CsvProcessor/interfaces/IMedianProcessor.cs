using erm.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace erm.CsvProcessor.interfaces
{
    public interface IMedianProcessor
    {
        Result<MedianResult> GetMedianResult(List<EnergyRecord> data);
    }
}
