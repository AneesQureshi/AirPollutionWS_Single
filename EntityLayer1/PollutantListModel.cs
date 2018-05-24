using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer1
{
    public class PollutantListModel
    {
        public double StationLatitude { get; set; }
        public double StationLongitude { get; set; }
        public string last_update { get; set; }
        public string timeZone { get; set; }
        public List<PollutantModel> pollutantModelList{ get; set; }
    }
}
