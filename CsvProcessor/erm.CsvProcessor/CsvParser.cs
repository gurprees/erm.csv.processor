using CsvHelper;
using erm.CsvProcessor.interfaces;
using erm.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace erm.CsvProcessor
{
    public class CsvParser: ICsvParser
    {
        public Result<List<EnergyRecord>> GetData(FileInfo fileInfo)
        {
            if (IsFileLocked(fileInfo))
                return new Result<List<EnergyRecord>>(false, null, $"File {fileInfo.Name} is in use by another process");

            var fileType = fileInfo.Name.Split('_')[0];

            using (var reader = new StreamReader(fileInfo.FullName))
            using (var csv = new CsvReader(reader))
            {
                switch (fileType)
                {
                    case "LP":
                        csv.Configuration.RegisterClassMap<LpMap>();
                        break;
                    case "TOU":
                        csv.Configuration.RegisterClassMap<TouMap>();
                        break;
                    default:
                        return new Result<List<EnergyRecord>>(false, null, $"Unknown file {fileInfo.Name}");
                }

                var csvResult = csv.GetRecords<EnergyRecord>().ToList();
                if (csvResult.Count < 1)
                    return new Result<List<EnergyRecord>>(false, null, $"No records found in {fileInfo.Name}");

                return new Result<List<EnergyRecord>>(true, csvResult, null);
            }
        }

        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return false;
        }
    }
}
