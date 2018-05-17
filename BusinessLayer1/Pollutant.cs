using DbLayer1;
using EntityLayer1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer1
{
    public class Pollutant
    {

        //fetch and add pollutants from pvt api on the basis of lat long 
        public void FetchAddPollutants(List<StationModel> objStationList)
        {
            List<PollutantListModel> objAllStationPollutantList = new List<PollutantListModel>();
            objAllStationPollutantList = FetchPollutants(objStationList);
            AddPollutants(objAllStationPollutantList);
        }

        //fetch pollutant records from pvt api on the basis of lat long 
        public List<PollutantListModel> FetchPollutants(List<StationModel> objStationList)
        {

            List<PollutantListModel> objAllStationPollutantList = new List<PollutantListModel>();

            ApiHelper objAp = new ApiHelper();

            foreach (var oneStation in objStationList)
            {
                List<PollutantModel> objStaionPollutant = new List<PollutantModel>();
                PollutantListModel oneStationList = new PollutantListModel();
                oneStationList = objAp.fetchPollutants(oneStation);
                
                objAllStationPollutantList.Add(oneStationList);
                
            }

            return objAllStationPollutantList;

        }

        //add pollutant records from pvt api on the basis of lat long to database

        public void AddPollutants(List<PollutantListModel> objAllStationPollutantList)
        {



            DbHelper objDb = new DbHelper();

            foreach (var oneStation in objAllStationPollutantList)
            {
                objDb.AddPollutants(oneStation);

            }



        }
    }
}
