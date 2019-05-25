using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace erm.CsvProcessor.interfaces
{
    public interface IProcessor
    {
        void ProcessFiles(FileInfo fileInfo);
    }
}
