using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbLayer1;
using EntityLayer1;

namespace BusinessLayer1
{
    public class Place
    {

        //fetching country, state, city name from govt api
        public List<RecordsModel> FetchPlace()
        {
            List<RecordsModel> objRecordsModel = new List<RecordsModel>();

            ApiHelper objAp = new ApiHelper();
            objRecordsModel= objAp.fetchPlace();
            return objRecordsModel;

        }

        //adding country, state, city name from govt api to database
        public void AddPlace(List<RecordsModel> objRecordsModel)
        {
            DbHelper objdb = new DbHelper();
            objdb.addPlace(objRecordsModel);

        }

       
        //fetching city details from database
        public List<RecordsModel> FetchCity()
        {
            List<RecordsModel> objCityList = new List<RecordsModel>();
            DbHelper objdb = new DbHelper();
            objCityList= objdb.fetchCity();
            return objCityList;
        }

        //fetching station details from Pvt Api
        public List<StationModel> FetchStation(string city)
        {
            List<StationModel> objStationList = new List<StationModel>();
            ApiHelper objAp = new ApiHelper();
            objStationList = objAp.fetchStation(city);
            return objStationList;
        }


       

        //add station name,aqi, lat long
        public void addStation(List<StationModel> objStationList, string cityId)
        {
            DbHelper objdb = new DbHelper();
            objdb.addStation(objStationList,cityId);

        }
        

    }
}
