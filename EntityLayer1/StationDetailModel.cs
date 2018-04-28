using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace EntityLayer1
{
    public class StationDetailModel
    {
        public string name { get; set; }
        public double[] geo { get; set; }

        //GeoCoordinateWatcher geo = new GeoCoordinateWatcher();

    }
}
