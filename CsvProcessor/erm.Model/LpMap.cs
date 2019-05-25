using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace erm.Model
{
    public class LpMap : ClassMap<EnergyRecord>
    {
        public LpMap()
        {
            Map(m => m.DataValue).Name("Data Value");
            Map(m => m.DateTime).Name("Date/Time");
        }
    }
}
