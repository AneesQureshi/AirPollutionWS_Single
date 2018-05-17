using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer1
{
    public class RecordsModel
    {
        public string id { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string station { get; set; }
        public string last_update { get; set; }
        public string pollutant_id { get; set; }
        public string pollutant_min { get; set; }
        public string pollutant_max { get; set; }
        public string pollutant_avg { get; set; }
        public string pollutant_unit { get; set; }

    }
}
