using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AirPollutionWS
{
    public partial class AirPollutionService : ServiceBase
    {
        public AirPollutionService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            AirPollutionMain.MainActivity();
        }

        protected override void OnStop()
        {
        }
    }
}
