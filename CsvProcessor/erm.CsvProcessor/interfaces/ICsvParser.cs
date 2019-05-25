using erm.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace erm.CsvProcessor.interfaces
{
    public interface ICsvParser
    {
        Result<List<EnergyRecord>> GetData(FileInfo fileInfo);
    }
}
