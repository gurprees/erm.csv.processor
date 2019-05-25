using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace erm.Model
{
    public class TouMap : ClassMap<EnergyRecord>
    {
        public TouMap()
        {
            Map(m => m.DataValue).Name("Energy");
            Map(m => m.DateTime).Name("Date/Time");
        }
    }
}