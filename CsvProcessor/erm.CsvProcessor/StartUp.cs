using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using erm.CsvProcessor.interfaces;
using erm.Model;
using Microsoft.Extensions.Options;

namespace erm.CsvProcessor
{
    public class StartUp : IStartUp
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IProcessor _processor;
        public StartUp(IOptions<AppSettings> settings, IProcessor processor)
        {
            _settings = settings;
            _processor = processor;
        }
        public void Start()
        {
            var files = Directory.GetFiles(_settings.Value.CsvPath, "*.csv");
            foreach (var file in files)
            {
                _processor.ProcessFiles(new FileInfo(file));
            }
        }
    }
}
